using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CreateChains : MonoBehaviour
{
    [SerializeField] private GameObject chainPrefab;
    [SerializeField] private float chainLinkSize = 0.5f;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private bool closeLoop = true; // Toggle for open or closed loop

    [Button("Create Chain")]
    private void CreateChainButton()
    {
        if (chainPrefab == null)
        {
            Debug.LogWarning("Assign chainPrefab first.");
            return;
        }
        if (waypoints.Count < 2)
        {
            Debug.LogWarning("Need at least 2 waypoints to create a chain.");
            return;
        }

        ClearChainLinks();

        for (int i = 0; i < waypoints.Count; i++)
        {
            if (!closeLoop && i == waypoints.Count - 1) break;

            Transform current = waypoints[i];
            Transform next = (i == waypoints.Count - 1) ? waypoints[0] : waypoints[i + 1];

            CreateChainSegment(current, next);
        }
    }
    private void CreateChainSegment(Transform start, Transform end)
    {
        float distance = Vector3.Distance(start.position, end.position);
        int numLinks = Mathf.CeilToInt(distance / chainLinkSize);

        for (int i = 0; i < numLinks; i++)
        {
            float t = numLinks > 1 ? (float)i / (numLinks - 1) : 0f;
            Vector3 linkPosition = Vector3.Lerp(start.position, end.position, t);
            Quaternion linkRotation = Quaternion.identity;

            Instantiate(chainPrefab, linkPosition, linkRotation, transform);
        }
    }
    private void ClearChainLinks()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    private void Start()
    {
        CreateChainButton();
    }
}
