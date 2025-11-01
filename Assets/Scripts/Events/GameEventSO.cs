using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "_Event", menuName = "Scriptable Objects/Events/GameEvent")]
public class GameEventSO : GameEventBaseSO
{
    private event UnityAction listeners;

    public void Raise() => listeners?.Invoke();
    public void RegisterListener(UnityAction listener) => listeners += listener;
    public void UnregisterListener(UnityAction listener) => listeners -= listener;
}
