using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Transform pivotPoint; // The pivot point (empty GameObject) for the pendulum
    [SerializeField] private float maxHeight = 2.0f; // Maximum height of the pendulum swing
    [SerializeField] private float speedOfAngleIncrease = 30.0f; // Speed of angle increase (adjust as needed)

    private float angle = 0.0f;

    private void Update()
    {
        // Update the angle based on time and speed
        angle += speedOfAngleIncrease * Time.deltaTime;

        // Ensure the angle stays within a reasonable range (e.g., -65 to 65 degrees)
        if (angle > 65 || angle < -65)
            speedOfAngleIncrease *= -1;

        // Calculate the end position of the pendulum
        Vector3 endPos = pivotPoint.position + (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.down * maxHeight);

        // Smoothly move the object to the end position
        transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime);
    }
}
