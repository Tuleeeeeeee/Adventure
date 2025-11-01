using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DynamicEventSystemHandler : MonoBehaviour
{
    [Header("References")]
    public List<Selectable> Selectables = new();

    [Header("Controls")]
    [SerializeField] protected InputActionReference navigateActionReference;

    [Header("Animations")]
    [SerializeField] protected float selectedAnimationScale = 1.1f;
    [SerializeField] protected float scaleDuration = 0.25f;

    [Header("Sounds")]
    [SerializeField] protected UnityEvent soundEvent;


    protected Dictionary<Selectable, Vector3> scales = new();
    private Selectable lastSelected;

    protected Tween scaleUpTween;
    protected Tween scaleDownTween;


    public virtual void OnEnable()
    {
        navigateActionReference.action.performed += OnNavigate;
    }

    public virtual void OnDisable()
    {
        navigateActionReference.action.performed -= OnNavigate;
        scaleUpTween?.Kill();
        scaleDownTween?.Kill();
    }

    protected virtual IEnumerator SelectAfterDelay()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(Selectables[0].gameObject);
    }

    protected virtual void AddSelectionListener(Selectable selectable)
    {
        EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry SelectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select
        };
        SelectEntry.callback.AddListener(OnSelect);
        trigger.triggers.Add(SelectEntry);

        EventTrigger.Entry DeselectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Deselect
        };
        DeselectEntry.callback.AddListener(OnDeselect);
        trigger.triggers.Add(DeselectEntry);

        EventTrigger.Entry PointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        PointerEnter.callback.AddListener(OnPointerEnter);
        trigger.triggers.Add(PointerEnter);

        EventTrigger.Entry PointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        PointerExit.callback.AddListener(OnPointerExit);
        trigger.triggers.Add(PointerExit);
    }

    protected virtual void OnSelect(BaseEventData eventData)
    {
        soundEvent?.Invoke();
        lastSelected = eventData.selectedObject.GetComponent<Selectable>();
        Vector3 newScale = eventData.selectedObject.transform.localScale * selectedAnimationScale;
        scaleUpTween = eventData.selectedObject.transform.DOScale(newScale, scaleDuration).SetEase(Ease.OutBack);
    }

    protected virtual void OnDeselect(BaseEventData eventData)
    {
        Selectable selectable = eventData.selectedObject.GetComponent<Selectable>();
        scaleDownTween = eventData.selectedObject.transform.DOScale(scales[selectable], scaleDuration).SetEase(Ease.OutBack);
    }

    protected virtual void OnPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            Selectable selectable = pointerEventData.pointerEnter.GetComponentInParent<Selectable>();
            if (selectable == null)
            {
                selectable = pointerEventData.pointerEnter.GetComponentInChildren<Selectable>();
            }

            pointerEventData.selectedObject = selectable.gameObject;
        }
    }

    protected virtual void OnPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            pointerEventData.selectedObject = null;
        }
    }

    protected virtual void OnNavigate(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected.gameObject);
        }
    }

    #region HELPER METHOD

    public void AddSelectable(Selectable selectable)
    {
        Selectables.Add(selectable);
    }

    public void InitSelectable()
    {
        foreach (var selectable in Selectables)
        {
            AddSelectionListener(selectable);
            scales.TryAdd(selectable, selectable.transform.localScale);
        }
    }
    public void SetFirstSlected()
    {
        StartCoroutine(SelectAfterDelay());
    }
    #endregion
}