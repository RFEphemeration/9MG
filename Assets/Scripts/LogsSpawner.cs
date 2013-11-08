using UnityEngine;
using System.Collections;

public class LogsSpawner : MonoBehaviour {
	
	
	public float delay = 10.0f;
	public GameObject log;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, delay);
	}
	
	void Spawn () {
		
		Vector3 position;
		
		float randomPos = Random.Range (0, 100);
		
		if (randomPos >= 75)
		{
			position = new Vector3(10, 3, Random.Range (-7,7));
			Instantiate(log, position, log.transform.rotation);
		}
		else if (randomPos >= 50)
		{
			position = new Vector3(-10, 3, Random.Range(-7, 7));
			Instantiate(log, position, log.transform.rotation);
		}
		else if (randomPos >= 25)
		{
			position = new Vector3(Random.Range(-7, 7), 3, 10);
			Instantiate(log, position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
		}
		else
		{
			position = new Vector3(Random.Range(-7, 7), 3, -10);
			Instantiate(log, position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
		}
		
	}
	
}
