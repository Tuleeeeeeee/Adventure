using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RockHead : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    Transform left, right, top, bottom;
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward);
    }
    /*private bool checkIfTouchingLeft()
    {
        return Physics2D.Raycast(left.position, 5f);
    }*/
    private void OnDrawGizmos()
    {
        /* Gizmos.color = Color.red;*/
        /*Gizmos.DrawLine(transform.position, transform.position * Vector2.left);*/
        //Right
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right *1.01f);
        //Up
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 1.01f);
        //Left
        Gizmos.color= Color.green;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.right * 1.01f);
        //Down
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * 1.01f);
    }

}
