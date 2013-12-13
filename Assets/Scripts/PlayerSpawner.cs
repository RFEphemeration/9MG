using UnityEngine;
using System.Collections;

public class PlayerSpawner : Spawner {
	
	public static bool respawn = false;
	
	public static int numDead;
	
	//player spawner will be told by the GUI how many players to spawn and will do just that.
	private int numPlayers = 2;
	//to do more players I needed to make it an array of onjects.
	
	// Use this for initialization
	void Start () {
		numDead = 0;
		for (int i = 0; i < numPlayers; i++)
		{
			theObjects[i].SendMessage("setID", i);
		}
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (numDead == numPlayers)
		{
			PauseMenuGUI.gameOver = true;
			numDead = 0;
		}
		if (respawn)
		{
			respawn = false;
			Spawn();
		}
		
	}
	
	void Spawn () {
		
		for (int i = 0; i < numPlayers; i++)
		{
			Vector3 position = new Vector3((i - 2) * 4, 0, 0);
			Instantiate(theObjects[i], position, Quaternion.identity);
		}
		
	}

	
}
