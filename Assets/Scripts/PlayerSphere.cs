using UnityEngine;
using System.Collections;

public class PlayerSphere : MonoBehaviour {
	
	public GameObject theSphere;
	
	private GameObject character;
	
	public static bool respawn = false;
	private int health;
	public int startingHealth;
	
	// Use this for initialization
	void Start () {
		health = startingHealth;
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
		
		health = startingHealth;
		Vector3 position = new Vector3(0, 0, 0);
		
		character = (GameObject) Instantiate(theSphere, position, Quaternion.identity);
		character.transform.parent = transform;
	}
	
	private void takeDamage()
	{
		health--;
		if (health <= 0)
			character.SendMessage("killMe");
	}
	
}
