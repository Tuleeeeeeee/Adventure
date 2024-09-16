using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Fruit : MonoBehaviour, ICollectible
{
    public enum CollectibleType
    {
        Fruit,
        Boost
    }

    [SerializeField]
    private int incrementValue = 0;

    private RuntimeAnimatorController collectedController;

    private Animator animator;

    public CollectibleType ItemType;

    public static event Action<int, CollectibleType> OnCollectiblesCollected;

    private void Awake()
    {
        var handle = Addressables.LoadAssetAsync<RuntimeAnimatorController>("Assets/Items/Fruits/Collected.controller");
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                collectedController = op.Result; 
            }
        };


        animator = GetComponent<Animator>();
    }
    public void OnCollected()
    {

        Debug.Log($"Hi You Have Collected {ItemType}");
        OnCollectiblesCollected?.Invoke(incrementValue, ItemType);
        animator.runtimeAnimatorController = collectedController;
        Destroy(gameObject, .5f);
    }
}


