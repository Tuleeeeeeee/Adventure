using UnityEngine;

public class FruitCollectible : Collectible
{
    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void OnCollected(GameObject collector)
    {
        anim.SetTrigger("Collected");
    }
}