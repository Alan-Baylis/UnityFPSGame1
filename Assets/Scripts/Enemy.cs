using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    public int health = 1;
    public AudioClip death;
    public AudioClip attack;
    public AudioSource audi;
    public MeshRenderer mr;
    public bool dead = false;
    public ParticleSystem ps;
    public SphereCollider sc;
    public CharacterController cc;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            //Go towards target
            MoveTowardsTarget(player.transform.position);
        }
    }

    void Update()
    {
        //Kill if 0 health
        if (health <= 0)
        {
            if (!dead)
            {
                Inventory.score++;
                Die(death);
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            health--;
        }
    }

    /* Doesn't work *thinking*
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
    */

    void MoveTowardsTarget(Vector3 target)
    {
        //Get the difference and move towards target
        Vector3 offset = target - transform.position;
        if (offset.magnitude > 2f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        //If at target do damage and die
        else
        {
            Die(attack);
            Inventory.health--;
        }
    }

    void Die(AudioClip clip)
    {
        //Spawn another enemy
        player.GetComponent<EnemySpawner>().SpawnEnemy();
        //Is dead
        dead = true;
        //Create death explosion
        ParticleSystem pSystem = Instantiate(ps, transform.position, transform.rotation);
        //Kill after 3 seconds
        Destroy(pSystem.gameObject, 3f);
        //Set audio
        audi.clip = clip;
        //Get rid of object to player perspective
        mr.enabled = false;
        sc.enabled = false;
        //Play audio
        audi.Play();
        //Kill whole object
        Destroy(gameObject, 5f);
        //Kill other collider after .1f (this one makes it so that the bullets still bounce off the enemy instead of just going right through them)
        Invoke("KillCC", .1f);
    }

    void KillCC()
    {
        cc.enabled = false;
    }
}
