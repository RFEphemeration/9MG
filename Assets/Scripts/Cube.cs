using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public Vector3 direction;
	public Material mat;
	public Material dead;
	private Material start;
	public static string[] states = {"spawn", "start", "follow", "leave", "dying"};
	public string state;
	private float changeTime;

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
		state = "dying";
	}
	
	void Update() {
		if (state == "dying") {
			renderer.material = dead;
		} else {
			
			renderer.material.Lerp(start, mat, 1.8f - transform.position.y);
		}
	}
	
	/*
	void OnCollisionEnter(Collision col) {
		Vector3 boom = rigidbody.position - col.gameObject.rigidbody.position;
		boom.Normalize();
		rigidbody.AddForce(boom * 100);
	}
	*/
}
