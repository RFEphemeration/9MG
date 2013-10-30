using UnityEngine;
using System.Collections;


public class PauseMenuGUI : MonoBehaviour {
	
	public GUISkin menuSkin;
	public int guiDepth = 0;
	public Rect menuArea;
	public Rect resumeButton;
	public Rect optionsButton;
	public Rect quitButton;
	public Rect soundFXButton;
	public Rect musicButton;
	public Rect backButton;
	Rect menuAreaNormalized;
	public static bool isPaused = false;
	public static bool gameOver = false;
	private bool optionsMenu;
	
	public bool networking = false;
	
	// Use this for initialization
	void Start () {
		optionsMenu = false;
	    menuAreaNormalized = new Rect(menuArea.x * Screen.width * 0.5f - (menuArea.width * 0.5f),
		menuArea.y * Screen.height * 0.5f - (menuArea.height * 0.5f), 
		menuArea.width, menuArea.height);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause")) {
			if (gameOver) {
				restart ();
			} else {
				if(!isPaused) {
					Time.timeScale = 0.0f;
					isPaused = true;
				} else {
					Time.timeScale = 1.0f;
					isPaused = false;
				}
			}
		}
	}
	
	
	void restart() {
		Time.timeScale = 0.0f;
				PlayerSphere.respawn = true;
				GameObject[] allcubes = GameObject.FindGameObjectsWithTag("Cube");
				foreach (GameObject cube in allcubes) {
					Destroy(cube);
				}
				isPaused = false;
				gameOver = false;
				Time.timeScale = 1.0f;
				ClockGUI.resetStartTime();
	}
	
	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.depth = guiDepth;
		if(isPaused && !optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Resume"))
			{
				Time.timeScale = 1.0f;
				isPaused = false;
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				optionsMenu = true;
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				StartCoroutine("ButtonAction", "quit");
			}
			GUI.EndGroup();
		}
		else if (gameOver && !optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Restart"))
			{	
				restart ();
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				optionsMenu = true;
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				//TODO
				//StartCoroutine("ButtonAction", "quit");
			}
			GUI.EndGroup();
		}
		else if (optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			
			if (GUI.Button(new Rect(soundFXButton), "Sound Fx"))
			{
				//TODO
			}
			if (GUI.Button(new Rect(musicButton), "Music"))
			{
				//TODO
			}
			if(GUI.Button(new Rect(backButton), "Back"))
			{
				optionsMenu = false;
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