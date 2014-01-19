	using UnityEngine;
using System.Collections;

public class Cubes : Spawner {
	public float delay = 1.0f;
	private float startTime;
	private bool pacingFlag;
	//public GameObject cube;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, delay);
	}
	
	//Just check every 30 seconds and make it harder and harder every 30 seconds.
	void FixedUpdate() {
		if (Time.time - startTime >= 20.0f && !pacingFlag) {
			startTime = Time.time;
			CancelInvoke("Spawn");
			pacingFlag = true;
		}
		
		if (Time.time - startTime >= 5.0f && pacingFlag) {
			pacingFlag = false;
			startTime = Time.time;
			delay -= 0.1f;
			InvokeRepeating("Spawn", delay, delay);
		}
	}

	void Spawn () {
		Vector3 position = new Vector3(Random.Range(10,-10), 3, Random.Range (10,-10));
		
		Instantiate(theObjects[0], position, Quaternion.identity);
	}
	

	
}
