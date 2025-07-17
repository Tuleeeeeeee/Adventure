using Tuleeeeee.Enums;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CollectibleEventSO", menuName = "Scriptable Objects/Events/Collectible Event")]
public class CollectibleEventSO : ScriptableObject
{
    [System.Serializable]
    public class CollectibleEvent : UnityEvent<CollectibleEventArgs> { }

    public CollectibleEvent OnCollectibleCollected;

    public void RaiseEvent(CollectibleEventArgs args)
    {
        OnCollectibleCollected?.Invoke(args);
    }
}

[System.Serializable]
public class CollectibleEventArgs
{
    public CollectibleType collectibleType;
    public int value;

    public CollectibleEventArgs(CollectibleType type, int val)
    {
        collectibleType = type;
        value = val;
    }
}
