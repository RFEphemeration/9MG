using UnityEngine;
using System.Collections;

public class ClockGUI : MonoBehaviour {
	
	
	public GUISkin menuSkin;
	public int guiDepth = 0;
	public Rect clockArea;
	public Rect clock;
	private static float startTime;
	private static float timeElapsed;
	Rect clockAreaNormalized;
	
	
	// Use this for initialization
	void Start () {
		
	    clockAreaNormalized = new Rect(clockArea.x * Screen.width * 0.5f - (clockArea.width * 0.5f), 0, 
		clockArea.width, clockArea.height);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!PauseMenuGUI.gameOver)
			timeElapsed = Time.time - startTime;
	}
	
	void OnGUI()
	{
		GUI.skin = menuSkin;
		GUI.depth = guiDepth;
		GUI.BeginGroup(clockAreaNormalized);
			GUI.Label(new Rect(clock), timeElapsed.ToString());
		GUI.EndGroup();
	}
	
	public static void resetStartTime()
	{
		startTime = Time.time;
	}
}
