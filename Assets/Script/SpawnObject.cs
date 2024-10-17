using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn; // The GameObject to spawn

    void Start()
    {
        SpawnAtPositions();
    }

    void SpawnAtPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Instantiate(objectToSpawn, transform.GetChild(i).position, Quaternion.identity, transform.GetChild(i));
        }
    }
}

