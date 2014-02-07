	using UnityEngine;
using System.Collections;

public class Cubes : Spawner {
	public float delay = 1.0f;
	private float startTime;
	private bool pacingFlag;
	
	public float spawnChange = 0.1f;
	public float roundTime = 30.0f;
	public float restTime = 5.0f;

	//public GameObject cube;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, delay);
	}
	
	//Just check every 30 seconds and make it harder and harder every 30 seconds.
	void FixedUpdate() {
		if (Time.time - startTime >= roundTime && !pacingFlag) {
			startTime = Time.time;
			CancelInvoke("Spawn");
			pacingFlag = true;
		}
		
		if (Time.time - startTime >= restTime && pacingFlag) {
			pacingFlag = false;
			startTime = Time.time;
			delay -= spawnChange;
			InvokeRepeating("Spawn", delay, delay);
		}
	}

	void Spawn () {
		Vector3 position = new Vector3(Random.Range(10,-10), 3, Random.Range (10,-10));
		
		Instantiate(theObjects[0], position, Quaternion.identity);
	}
	

	
}
