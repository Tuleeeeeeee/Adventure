using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void DeactiveObject()
    {
        transform.gameObject.SetActive(false);
    }
}
