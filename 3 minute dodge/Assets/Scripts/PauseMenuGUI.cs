using UnityEngine;
using System.Collections;


public class PauseMenuGUI : MonoBehaviour {
	
        public GUISkin menuSkin;
        public int guiDepth = 0;
        public Rect menuArea;
        public Rect resumeButton;
        public Rect optionsButton;
        public Rect quitButton;
        Rect menuAreaNormalized;
        public static bool isPaused = false;
        public static bool gameOver = false;
        
        public bool networking = false;
        
        // Use this for initialization
        void Start () {
                menuAreaNormalized = new Rect(menuArea.x * Screen.width * 0.5f - (menuArea.width * 0.5f),
			menuArea.y * Screen.height * 0.5f - (menuArea.height * 0.5f), 
			menuArea.width, menuArea.height);
        }
        
        // Update is called once per frame
        void Update () {
                if(Input.GetKeyDown(KeyCode.Escape) && !gameOver)
                {
                        if(!isPaused)
                        {
                        	Time.timeScale = 0.0f;
                        	isPaused = true;
                        }
                        else
                        {
                                Time.timeScale = 1.0f;
                                isPaused = false;
                        }
                }
			if (gameOver)
			{
				Time.timeScale = 0.0f;
			}
        }
        
	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.depth = guiDepth;
		if(isPaused)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Resume"))
			{
				Time.timeScale = 1.0f;
				isPaused = false;
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				//Nothing here yet.
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				StartCoroutine("ButtonAction", "quit");
			}
			GUI.EndGroup();
		}
		if (gameOver)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Restart"))
			{
				Time.timeScale = 1.0f;
				PlayerSphere.respawn = true;
				isPaused = false;
				gameOver = false;
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				//Nothing here yet.
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				StartCoroutine("ButtonAction", "quit");
			}
			GUI.EndGroup();
		}
                    
	}
}
  
  /*  
                        else if(menuPage == "options")
                        {
                                music = GUI.Toggle(new Rect(musicToggle), music, "Music");
                               // sfx = GUI.Toggle(new Rect(sfxToggle), sfx, "SFX");
                               // instructionsOn = GUI.Toggle (new Rect(toggleInstruction), instructionsOn, "Instructions");
                                if(GUI.Button(new Rect(quitButton), "Back"))
                                {
                                        audio.PlayOneShot(click);
                                        menuPage = "main";
                                }
                        }
                        GUI.EndGroup();*/
    /*    IEnumerator ButtonAction(string levelName) {
                audio.PlayOneShot(click);
                Time.timeScale = 1.0f;
                yield return new WaitForSeconds(0.35f);
                if(levelName != "quit")
                {
                        Application.LoadLevel(levelName);
                }
                else
                {
                        Application.Quit();
                        Debug.Log("Quit!");
                }
        }*/