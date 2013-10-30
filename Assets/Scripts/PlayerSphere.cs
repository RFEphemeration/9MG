using UnityEngine;
using System.Collections;

public class PlayerSphere : MonoBehaviour {
	
	public GameObject theSphere;
	
	private GameObject character;
	
	private static float SAFETIME = 1f;
	
	public static bool respawn = false;
	private int shields;
	public int startingShields;
	private float hitTime;
	
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
		
		character = (GameObject) Instantiate(theSphere, position, Quaternion.identity);
		character.transform.parent = transform;
	}
	
	private void takeDamage()
	{
		if (Time.time > hitTime) {
			if (shields <= 0)
				character.SendMessage("killMe");
			shields--;
			hitTime = Time.time + SAFETIME;
			character.SendMessage("hitMe", hitTime);
		}
	}
	
	
}
