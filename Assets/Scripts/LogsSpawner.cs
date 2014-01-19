using UnityEngine;
using System.Collections;

public class LogsSpawner : Spawner {
	
	
	public float delay = 30.0f;
	private float startTime;
	private bool pacingFlag;
	//public GameObject log;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		pacingFlag = false;
		InvokeRepeating("Spawn", delay, delay);
	}

	//Just check every 30 seconds and make it harder and harder every 30 seconds.
	void FixedUpdate() {
		if (Time.time - startTime >= 30.0f && !pacingFlag) {
			startTime = Time.time;
			CancelInvoke("Spawn");
			pacingFlag = true;
		}

		if (Time.time - startTime >= 3.0f && pacingFlag) {
			pacingFlag = false;
			startTime = Time.time;
			delay -= 3.0f;
			InvokeRepeating("Spawn", delay, delay);
		}
	}

	void Spawn () {
		
		Vector3 position;
		
		float randomPos = Random.Range (0, 100);
		
		if (randomPos >= 75)
		{
			position = new Vector3(10, 3, Random.Range (7,-7));
			Instantiate(theObjects[0], position, theObjects[0].transform.rotation);
		}
		else if (randomPos >= 50)
		{
			position = new Vector3(-10, 3, Random.Range(7, -7));
			Instantiate(theObjects[0], position, theObjects[0].transform.rotation);
		}
		else if (randomPos >= 25)
		{
			position = new Vector3(Random.Range(7, -7), 3, 10);
			Instantiate(theObjects[0], position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
		}
		else
		{
			position = new Vector3(Random.Range(7, -7), 3, -10);
			Instantiate(theObjects[0], position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
		}
		
	}
	
}
