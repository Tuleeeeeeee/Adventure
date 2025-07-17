using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private MonoBehaviour itemPrefab;
    [SerializeField] private string dictionaryEntry;

    void Start()
    {
        SetUpPool();
    }

    private void SetUpPool()
    {
        ObjectPool.SetupPool(itemPrefab, 10, dictionaryEntry);
    }
}