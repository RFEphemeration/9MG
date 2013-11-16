using UnityEngine;
using System.Collections;

public class Player : Character {
	
	//private GameObject character;
	
	private static float SAFETIME = 1f;
	
	private int shields;
	public int startingShields;	
	private float safeTime;
	
	public Material mat;
	private Material start;
	private Teleportor teleportor;
	
	// Use this for initialization
	void Start () {
		gameObject.tag = "Player";
		boom();
		start = new Material(renderer.material);
		teleportor = new Teleportor();
	}
	
	// Update is called once per frame
	void Update() {
		renderer.material.Lerp(start, mat, safeTime - Time.time);
	}
	
	void FixedUpdate() {
		Vector3 teleport = teleportor.GetTeleport();
		if (teleport.y <= 9000 ) {
			rigidbody.velocity = Vector3.zero;
			transform.position += teleport;
			boom();
		}
		
		if (transform.position.y < -5) killMe();
		
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cube") {
			boom();
			if (Time.time > safeTime) {
				if (shields <= 0)
					killMe();
				shields--;
				safeTime = Time.time + SAFETIME;
			}
		}
	}
	
	public void boom(float range = 3f, float magnitude = 400f) {
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag ("Cube");
		foreach (GameObject child in allObjects) {
    		float dist = (transform.position - child.transform.position).magnitude;
    		if (dist < range) {
				Vector3 boom = ((child.rigidbody.position - transform.position).normalized + Vector3.up * 0.5f).normalized;
				child.rigidbody.velocity = Vector3.zero;
				if (child.GetComponent<Cube>())
				{
					child.GetComponent<Cube>().state = "leave";
				}
				child.rigidbody.AddForce(boom * magnitude);
				boom.y = 0;
				boom.Normalize();
				if (child.GetComponent<Cube>())
				{
					child.GetComponent<Cube>().direction = boom;
				}
			}
    	}
	}
	
	void killMe() {
		//Destroy
		Destroy(gameObject);
		//Pause and give option to reset.
		//tell the pause menu to pause
		PauseMenuGUI.gameOver = true;
	}
	
	void reset()
	{
		shields = startingShields;
		safeTime = Time.time + SAFETIME;
	}
	
}
