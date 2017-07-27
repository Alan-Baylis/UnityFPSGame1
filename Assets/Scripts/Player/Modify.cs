using UnityEngine;
using System.Collections;

public class Modify : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    Ray ray;
    public GameObject bullet;

    void Start()
    {
    }
    void Update()
    {
        CollectBricks();
        Shoot();
    }

    //Function which allows player to collect ammo
    void CollectBricks()
    {
        //If ray from camera hits block then collect it
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                EditTerrain.SetBlock(hit, new BlockAir());
                Inventory.ammo += 1;
            }
        }
    }

    //Function which allows player to shoot collected cubes
    void Shoot()
    {
        //Use a bullet to shoot and only shoot if you have bullets
        Vector3 spawnPos = player.transform.position;
        Quaternion spawnRot = player.transform.rotation;
        if (Input.GetMouseButtonDown(0) && Inventory.ammo >= 1)
        {
            Instantiate(bullet, spawnPos, spawnRot);
            Inventory.ammo--;
        }
    }
}