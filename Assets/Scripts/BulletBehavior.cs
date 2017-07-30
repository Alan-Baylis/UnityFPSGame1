using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public Rigidbody rigi;
    public float thrust = 1000;
    public float upThrust = 50;

    void Awake()
    {
        //Get rigidbody
        rigi = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start()
    {
        //Add force to the bullet to make it move
        //transform.Translate((Vector3.forward * 4) * Time.deltaTime);
        rigi.AddRelativeForce(Vector3.forward * thrust);
        rigi.AddRelativeForce(Vector3.left * upThrust);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

    }
}
