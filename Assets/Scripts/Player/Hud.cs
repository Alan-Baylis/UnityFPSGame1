using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public Texture2D crosshairImage;
    private float scrW, scrH;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

        //GUI.DrawTexture(new Rect(Screen.width / 2 - scrW, Screen.height / 2 - scrH, scrW, scrH), crosshairImage);
        Reticule();
        StatDisplay();
    }
    void Reticule()
    {
        float width = 500 / 8;
        float height = 500 / 8;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height), crosshairImage);
    }
    void StatDisplay()
    {
        GUI.Box(new Rect(scrW, scrH, scrW, scrH), Inventory.ammo.ToString());
    }
}
