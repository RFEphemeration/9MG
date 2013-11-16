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
		
		shields = startingShields;
		hitTime = Time.time;
		Vector3 position = new Vector3(0, 0, 0);
		
		theObject = (GameObject) Instantiate(theSphere, position, Quaternion.identity);
		theObject.transform.parent = transform;
	}
}
