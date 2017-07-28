using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 2f;
    public float spawnRadius = 50f;

    void Start()
    {
        //Spawn enemy every spawnTime seconds
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn enemy
    public void SpawnEnemy()
    {
        GameObject clone = Instantiate(enemy);
        Vector3 randomPos = transform.position + Random.onUnitSphere * spawnRadius;
        clone.transform.position = randomPos;
        //Set enemy target
        clone.GetComponent<Enemy>().player = gameObject;
    }
}
