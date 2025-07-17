using System.Collections;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField]
    private float fanCooling = 5f;
    [SerializeField]
    private ParticleSystem wind;
    private AreaEffector2D windEffector;
    private Animator animator;
    void Start()
    {
        windEffector = GetComponent<AreaEffector2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(FanController(fanCooling));
    }
    IEnumerator FanController(float cooling)
    {
        wind.Play();
        windEffector.enabled = true;
        animator.SetBool("On", true);
        yield return new WaitForSeconds(cooling);
        wind.Stop();
        windEffector.enabled = false;
        animator.SetBool("On", false);
        yield return new WaitForSeconds(cooling);
        StartCoroutine(FanController(fanCooling));
    }
}
