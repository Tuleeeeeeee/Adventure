using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rotaion : MonoBehaviour
{
    [SerializeField] private float speed = 2f;



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
