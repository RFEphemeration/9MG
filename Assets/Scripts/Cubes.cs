using UnityEngine;
using System.Collections;

public class Cubes : Spawner {
	public float delay = 0.3f;
	//public GameObject cube;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, delay);
	}
	
	void Spawn () {
		Vector3 position = new Vector3(Random.Range(10,-10), 3, Random.Range (10,-10));
		
		Instantiate(theObjects[0], position, Quaternion.identity);
	}
	

	
}
