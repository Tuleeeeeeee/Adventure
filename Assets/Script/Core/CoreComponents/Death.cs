using UnityEngine;

public class Death : CoreComponent
{
    /*    [SerializeField] 
        private GameObject[] deathParticles;*/

    // private ParticleManager ParticleManager =>
    //      particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

    // private ParticleManager particleManager;

    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;

    private Animator animator;
    private void Start()
    {
        animator = core.transform.parent.GetComponent<Animator>();
    }
    public void Die()
    {
        /*    foreach (var particle in deathParticles)
            {
                //ParticleManager.StartParticles(particle);
            }*/
        animator.SetBool("hit", true);
        Invoke(nameof(setActive), 2f);
    }
    private void setActive()
    {
        core.transform.parent.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
