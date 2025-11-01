using UnityEngine.Events;

public abstract class GameEventSO<T> : GameEventBaseSO
{
    private event UnityAction<T> listeners;

    public void Raise(T value) => listeners?.Invoke(value);
    public void RegisterListener(UnityAction<T> listener) => listeners += listener;
    public void UnregisterListener(UnityAction<T> listener) => listeners -= listener;
}
