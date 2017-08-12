using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float scrW, scrH;
    public GameObject oldCamera;
    public GameObject newCamera;
    public bool showTut = false;
    public bool started = false;
    public bool loaded = false;
    public bool inOptions = false;
    public World world;
    public Vector3 newCameraPos;
    public FirstPersonCamera fpc;

    //Options menu member variables
    public AudioSource audi;
    public Vector2 resScrollPosition = Vector2.zero;
    public float audioSlider;
    //Resolution drop down menu will be set to screen resolution
    private string buttonName = Screen.width + "x" + Screen.height;
    private bool showResOptions = false;
    private bool fullscreenToggle;

    // Use this for initialization
    void Awake()
    {
        //Set audio source
        audi = newCamera.GetComponent<AudioSource>();
        newCameraPos = newCamera.transform.position;
    }

    void Start()
    {
        // Set toggle to current fullscreen status
        fullscreenToggle = Screen.fullScreen;

        // If there are player prefs load them in
        if (PlayerPrefs.HasKey("volume"))
        {
            // Set audio
            audioSlider = PlayerPrefs.GetFloat("volume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //After loading 500 chunks, game has loaded
        if (world.chunks.Count > 100)
        {
            loaded = true;
        }

        //Show menu if you press escape
        if (started && Input.GetKeyDown(KeyCode.Escape) && Inventory.health > 0)
        {
            InGameOptions();
        }

        if (audioSlider != audi.volume)
        {
            audi.volume = audioSlider;
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
        else if (showTut)
        {
            TutorialScreen();
        }
        else if (inOptions)
        {
            OptionsMenu();

            if (showResOptions)
            {
                ResOptionsFunc();
            }
        }
        //If loading is done show the start menu
        else if (!started && loaded)
        {
            StartMenu();
        }



        //If dead show gameover screen
        if (Inventory.health <= 0 && started)
        {
            GameOver();
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
            showTut = true;
        }
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 5.5f, scrW * 2, scrH), "Options"))
        {
            inOptions = true;
        }
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 7, scrW * 2, scrH), "Quit"))
        {
            Application.Quit();
        }

    }

    //Explain how to play 
    void TutorialScreen()
    {
        GUI.Box(new Rect(scrW * 7, scrH * 4, scrW * 3, scrH * .5f), "Right click blocks to collect ammo\nLeft click to shoot");
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 5.5f, scrW * 2, scrH), "Start for real"))
        {
            showTut = false;
            StartGame();
        }
    }

    //Show options that the player can change
    void OptionsMenu()
    {
        GUI.Box(new Rect(scrW, scrH, scrW * 14, scrH * 6), "Options");

        GUI.Box(new Rect(scrW * 2, scrH * 2, scrW * 4, scrH * 4), "Graphics");

        GUI.Box(new Rect(scrW * 6, scrH * 2, scrW * 4, scrH * 4), "Sound");
        //Used to have more options... R.I.P
        GUI.BeginGroup(new Rect(scrW * 6.5f, scrH * 2.5f, scrW * 4, scrH * 4));
        audioSlider = GUI.HorizontalSlider(new Rect(0, scrH, 3 * scrW, .5f * scrH), audioSlider, 0f, 1f);
        GUI.EndGroup();

        GUI.Box(new Rect(scrW * 10, scrH * 2, scrW * 4, scrH * 4), "Screen");

        GUI.BeginGroup(new Rect(scrW * 10.5f, scrH * 2.5f, scrW * 3, scrH * 5f));

        if (GUI.Button(new Rect(0, 0, scrW * 3, scrH * .5f), buttonName))
        {
            // Show res dropdown menu
            showResOptions = !showResOptions;
        }

        // Only show fullscreen toggle when resolution dropdown isn't shown (to avoid accidental toggling when in dropdown)
        if (!showResOptions)
        {
            fullscreenToggle = GUI.Toggle(new Rect(0, scrH, scrW * 3, scrH * .5f), fullscreenToggle, "Toggle Fullscreen");

            Screen.fullScreen = fullscreenToggle;
        }

        GUI.EndGroup();

        if (GUI.Button(new Rect(scrW * 7f, scrH * 8, scrW * 2, scrH), "Save & Back"))
        {
            if (started)
            {
                InGameOptions();
            }
            else
            {
                // Go back to previous menu and also save options
                SaveOptions();
                inOptions = false;
                showResOptions = false;
            }

        }
    }

    void ResOptionsFunc()
    {
        // Set up resolutions for button labels
        string[] res = new string[] { "1024×576", "1152×648", "1280×720", "1280×800", "1366×768", "1440×900", "1600×900", "1680×1050", "1920×1080", "1920×1200", "2560×1440", "2560×1600", "3840×2160" };

        // Set up resolution values to set (TODO could be improved)
        int[] resW = new int[] { 1024, 1152, 1280, 1280, 1366, 1440, 1600, 1680, 1920, 1920, 2560, 2560, 3840 };
        int[] resH = new int[] { 576, 648, 720, 800, 768, 900, 900, 1050, 1080, 1200, 1440, 1600, 2160 };

        // Create GUI style solid black (kek) for scrollable resolutions
        Texture2D black = new Texture2D(1, 1);
        black.SetPixel(1, 1, Color.black);
        GUIStyle solidBlack = new GUIStyle();
        solidBlack.normal.background = black;


        // Group for the drop down menu
        GUI.BeginGroup(new Rect(scrW * 10.5f, scrH * 3, scrW * 3, scrH * 4));

        resScrollPosition = GUI.BeginScrollView(new Rect(0, 0, scrW * 3, scrH * 4), resScrollPosition, new Rect(0, 0, scrW * 2.6f, scrH * 13));

        GUI.Box(new Rect(0, 0, scrW * 3, scrH * 13), "", solidBlack);

        for (int i = 0; i < 13; i++)
        {
            if (GUI.Button(new Rect(0, scrH * i, scrW * 2.7f, scrH), res[i]))
            {
                // Set resolution based on which button was pressed (array[i] name, array[i] width, array[i] height)
                Screen.SetResolution(resW[i], resH[i], Screen.fullScreen);
                buttonName = res[i];
                showResOptions = false;
            }
        }

        GUI.EndScrollView();

        GUI.EndGroup();
    }

    //Call this if in game to show/hide cursor
    void InGameOptions()
    {
        SaveOptions();
        fpc.inMenu = !fpc.inMenu;
        inOptions = !inOptions;
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Save all options
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("volume", audioSlider);
    }

    //Start Game
    void StartGame()
    {
        Time.timeScale = 1;
        oldCamera.SetActive(false);
        newCamera.SetActive(true);
        started = true;
        //Set player stats
        //Constant ammo count
        Inventory.ammo = 0;
        //Player health
        Inventory.health = 10;
        //Player score
        Inventory.score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpc.inMenu = false;
    }

    //End Game
    void GameOver()
    {
        fpc.inMenu = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GUI.Box(new Rect(scrW * 7, scrH * 4, scrW * 3, scrH * .5f), "Game Over! Your score was: " + Inventory.score);
        if (GUI.Button(new Rect(scrW * 7.5f, scrH * 5.5f, scrW * 2, scrH), "Back to menu"))
        {
            //Reset player position
            newCamera.transform.position = newCameraPos;
            //Enable mainmenu camera and disable player
            oldCamera.SetActive(true);
            newCamera.SetActive(false);
            //Set up menus again
            showTut = false;
            started = false;
            inOptions = false;
        }
    }

}
