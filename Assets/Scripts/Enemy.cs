using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    public int health = 5;
    CharacterController cc;
    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Go towards target
        MoveTowardsTarget(player.transform.position);
        //Kill if 0 health
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other)
    {
        //If spawns or collides with the ground
        if (other.collider.name == "Chunk(Clone)")
        {
            //Spawn another enemy if this enemy died in the ground
            player.GetComponent<EnemySpawner>().SpawnEnemy();
            Destroy(gameObject);
        }
    }

    void MoveTowardsTarget(Vector3 target)
    {
        //Get the difference and move towards target
        Vector3 offset = target - transform.position;
        if (offset.magnitude > 2f)
        {
            offset = offset.normalized * moveSpeed;
            cc.Move(offset * Time.deltaTime);
        }
        //If at target do damage and die
        else
        {
            Destroy(gameObject);
            Inventory.health--;
        }
    }
}
