using UnityEngine;
using System.Collections;

public class Cube : Enemy {
	public Vector3 direction;
	public Material mat;
	public Material dead;
	private Material start;
	new public static string[] states = {"spawn", "start", "follow", "leave", "dying"};
	private float changeTime;
	private bool firstPush;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Cube";
		float scale = 1.2f - ((float)Random.Range(0,100))/120f;
		transform.localScale = new Vector3(scale, scale, scale);
		direction = new Vector3(Random.Range(-100,100), 0, Random.Range (-100,100));
		direction.Normalize();
		start = new Material(renderer.material);
		state = Cube.states[0];
		changeTime = Time.time + 0.6f;
		firstPush = true;
	}
	
	// FixedUpdate is called once per timestep
	void FixedUpdate () {
		// rigidbody.AddForce(direction);
		if (transform.position.y < -5) {
			Destroy(gameObject);
		}
		if (state == "follow") {
			Vector3 towardPlayer = findClosestPlayer() - transform.position;
			towardPlayer.Normalize();
			//rigidbody.velocity = (towardPlayer * 4);
			rigidbody.AddForce(towardPlayer * 20);
			if (Time.time > changeTime) state = "leave";
		} else if (state == "start") {
			rigidbody.AddForce(direction * 20);
			if (Time.time > changeTime) {
				state = "follow";
				changeTime += 3f;
			}
			if (firstPush) {
				rigidbody.AddForce(transform.position * -1); // toward center
				firstPush = false;
			}
		} else if (state == "leave") {
			rigidbody.AddForce(direction * 20);
		} else if (state == "dying") {
			transform.position += Vector3.up * 3;
			if (transform.position.y > 4) Destroy (gameObject);
		} else if (state == "spawn") {
			if (Time.time > changeTime){
				state = "start";
				changeTime += 1f;
			}
		}
	}
	
	Vector3 findClosestPlayer() {
		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		float distance = Mathf.Infinity;
		Vector3 other = Vector3.zero;
		foreach (GameObject player in allPlayers) {
			Vector3 offset = player.transform.position - transform.position;
			float sqrLen = offset.sqrMagnitude;
			if (sqrLen < distance) {
				distance = sqrLen;
				other = player.transform.position;
			}
		}
		return other;
	}
	
	void killMe() {
		rigidbody.velocity = Vector3.zero;
		rigidbody.collider.enabled = false;
		state = "dying";
	}
	
	void Update() {
		if (state == "dying") {
			renderer.material = dead;
		} else {
			renderer.material.Lerp(start, mat, 1.8f - transform.position.y);
		}
	}
	
	
	void OnCollisionEnter(Collision col) {
		if (col.relativeVelocity.magnitude > 20 && col.gameObject.tag == "Cube") {
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce(Vector3.up * 400);
		}
	}
	
}
