using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static bool showHud = true;
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
        //Other scripts can disable showHud
        if (showHud)
        {
            //GUI.DrawTexture(new Rect(Screen.width / 2 - scrW, Screen.height / 2 - scrH, scrW, scrH), crosshairImage);
            Reticule();
            StatDisplay();
        }
    }
    //Reticule in centre of screen
    void Reticule()
    {
        float width = 500 / 8;
        float height = 500 / 8;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height), crosshairImage);
    }
    //Stats on screen
    void StatDisplay()
    {
        GUI.Box(new Rect(scrW, scrH, scrW, scrH * .5f), "Score: " + Inventory.score.ToString());
        GUI.Box(new Rect(scrW, scrH * 2, scrW, scrH * .5f), "Ammo: " + Inventory.ammo.ToString());
        GUI.Box(new Rect(scrW, scrH * 3, scrW, scrH * .5f), "Health: " + Inventory.health.ToString());
    }
}
