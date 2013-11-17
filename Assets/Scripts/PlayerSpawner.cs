using UnityEngine;
using System.Collections;

public class PlayerSpawner : Spawner {
	
	public static bool respawn = false;
	
	private GameObject player;
	
	
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

		Vector3 position = new Vector3(0, 0, 0);
		player = (GameObject) Instantiate(theObject, position, Quaternion.identity);
	}
}
