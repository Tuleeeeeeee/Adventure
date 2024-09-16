using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/Enemies State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
}
