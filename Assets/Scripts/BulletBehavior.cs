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
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        //If enemy reduce health
        if (other.collider.name == "Enemy(Clone)")
        {
            other.gameObject.GetComponent<Enemy>().health--;
        }
    }
    void OnTriggerEnter(Collider other)
    {

    }
}
