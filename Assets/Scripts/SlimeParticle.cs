using UnityEngine;

public class SlimeParticle : MonoBehaviour
{
    private void PutInPool()
    {
        ObjectPool.EnqueueObject(this, "slime");
    }
}
