using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/Enemies State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}