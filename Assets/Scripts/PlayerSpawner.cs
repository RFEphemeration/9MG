using UnityEngine;
using System.Collections;

public class PlayerSpawner : Spawner {
	
	public static bool respawn = false;
	
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (respawn)
		{
			respawn = false;
			Spawn();
		}
		
	}
	
	void Spawn () {
		theObject.SendMessage("reset"); 
		
		
		Vector3 position = new Vector3(0, 0, 0);
		Instantiate(theObject, position, Quaternion.identity);
	}
}
