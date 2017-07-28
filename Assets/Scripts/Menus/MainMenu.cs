using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private float scrW, scrH;
    public GameObject oldCamera;
    public GameObject newCamera;
    public bool started = false;
    public bool loaded = false;
    public World world;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //After loading 500 chunks, game has loaded
        if (world.chunks.Count > 100)
        {
            loaded = true;
        }
    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        //Show loading screen if loading
        if (!loaded)
        {
            LoadingScreen();
        }
        //If loading is done show the start menu
        else if (!started && loaded)
        {
            StartMenu();
        }

    }

    //First screen player sees when they enter the game; allows level to load
    void LoadingScreen()
    {
        GUI.Box(new Rect(scrW * 7, scrH * 2, scrW * 3, scrH), "Loading");
    }

    //Menu which player sees when they finish loading into the game
    void StartMenu()
    {
        GUI.Box(new Rect(scrW * 7, scrH * 2, scrW * 3, scrH), "FPS Game!");
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 4, scrW * 2, scrH), "Start"))
        {
            oldCamera.SetActive(false);
            newCamera.SetActive(true);
            started = true;
        }
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 5.5f, scrW * 2, scrH), "Options"))
        {

        }
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 7, scrW * 2, scrH), "Quit"))
        {

        }
        /*
		string[] menuItems = { "Start", "Options", "Exit" };
        for (int i = 0; i < 3; i++)
        {
            if (GUI.Button(new Rect(scrW * 7.5f, scrH * 1.5f * i + (scrH * 4), scrW * 2, scrH), menuItems[i]))
            {

            }
        }
		*/

    }
}
