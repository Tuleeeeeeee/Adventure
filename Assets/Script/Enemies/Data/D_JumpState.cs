using UnityEngine;

[CreateAssetMenu(fileName = "newJumpStateData", menuName = "Data/Enemies State Data/Jump State")]
public class D_JumpState : ScriptableObject
{
    public float jumpVelocity = 5f;
    public float movementSpeed = 3f;
    public int amountOfJumps = 1;
}
