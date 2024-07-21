using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Pendulum : MonoBehaviour
{
    private float angle = Mathf.PI / 4;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private float length = 3f;

    [SerializeField]
    private float angleV = 0;
    [SerializeField]
    private float angleA = 0;


    private Vector2 bob;


    private void Update()
    {
        angle += angleV;
        // angleV += angleA;

        bob.x = length * Mathf.Sin(angle) + origin.position.x;
        bob.y = length * Mathf.Cos(angle) + origin.position.y;

        transform.rotation = origin.rotation * Quaternion.Euler(0, 0, angle);
    }
    private void OnDrawGizmos()
    {

    }
}



