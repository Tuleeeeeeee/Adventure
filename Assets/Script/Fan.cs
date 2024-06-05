using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField]
    private float fanCooling = 5f;
    [SerializeField]
    private ParticleSystem wind;
    private AreaEffector2D windEffector;
    private Animator animator;

    /*
        private bool is_active;
        private float startTime;
        private float duration = 5f;*/
    // Start is called before the first frame update
    void Start()
    {
        windEffector = GetComponent<AreaEffector2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(FanController(fanCooling));
    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }
    void Update()
    {
        /*  if (is_active)
          {
              if (Time.time - startTime >= duration)
              {
                  is_active = false;
              }
          }
          else
          {
              if (Time.time - startTime >= duration)
              {
                  is_active = true;
                  startTime = Time.time; // Reset start time
              }
          }*/
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
