using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/Enemies State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public int projectileDamage = 100;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;
}
