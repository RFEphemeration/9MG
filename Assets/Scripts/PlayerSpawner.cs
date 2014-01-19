using UnityEngine;
using System.Collections;

public class PlayerSpawner : Spawner {
	
	public static bool respawn = false;
	
	public static int numDead;
	
	private GameObject[] theInstances;
	
	//player spawner will be told by the GUI how many players to spawn and will do just that.
	private int numPlayers = 1;
	//to do more players I needed to make it an array of onjects.
	
	// Use this for initialization
	void Start () {
		numDead = 0;
		theInstances = new GameObject[theObjects.Length];
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
			Vector3 position = new Vector3((i - 1) * 4, 0, 0);
			theInstances[i] = Instantiate(theObjects[i], position, Quaternion.identity) as GameObject;
			theInstances[i].SendMessage("setPlayerID", i+1);
			theInstances[i].SendMessage("setTeleporterID", i+1);
		}
		
	}

	
}
