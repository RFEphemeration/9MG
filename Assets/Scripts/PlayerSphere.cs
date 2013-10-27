using UnityEngine;
using System.Collections;

public class PlayerSphere : MonoBehaviour {
	
	public GameObject theSphere;
	
	public static bool respawn = false;
	
	// Use this for initialization
	void Start () {

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
		
		Instantiate(theSphere, position, Quaternion.identity);
	}
	
}
