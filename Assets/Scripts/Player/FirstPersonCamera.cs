using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public bool inMenu = false;

    void Update()
    {
        if (!inMenu)
        {
            Rotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Rotation()
    {
        // Move camera based on getAxis
        yaw += speedH * Input.GetAxis("Mouse X");
        // Lock camera so you can't 360
        pitch = Mathf.Min(80, Mathf.Max(-80, pitch + -Input.GetAxis("Mouse Y")));

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.parent.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
        transform.parent.rotation = Quaternion.Euler(new Vector3(0f, transform.parent.rotation.eulerAngles.y, 0f));
    }
}
