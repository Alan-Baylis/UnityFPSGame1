using UnityEngine;
using System.Collections;

public class Modify : MonoBehaviour
{
    public Camera cam;
    Ray ray;

    void Start()
    {
    }
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                EditTerrain.SetBlock(hit, new BlockAir());
            }
        }
    }
}