using UnityEngine;
[CreateAssetMenu(fileName = "newFlyStateData", menuName = "Data/Enemies State Data/Fly State")]
public class D_FlyState : ScriptableObject
{

    [Header("Path")]
    public Vector2[] ellipses = new Vector2[]
{
        new Vector2(5f, 3f), // First ellipse (a=5, b=3)
};

    public float speed = 2f; // Speed of the movement

    public Vector2 center = new Vector2(0f, 0f); // Center of the ellipse

    public int pathIndex = 0;

    public float switchCooldown = 5f; // Time between path switches
    public float timeSinceLastSwitch = 0f;
    public float angle = 0f;
    public int currentPath = 0;
    public Vector2 targetPosition;
}
