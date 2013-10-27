using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public Vector3 direction;
	public bool lethal;
	public Material mat;
	private Material start;
	private float t;
	private float chaseStart;
	private float chaseEnd;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Cube";
		float scale = 1.2f - ((float)Random.Range(0,100))/120f;
		transform.localScale = new Vector3(scale, scale, scale);
		direction = new Vector3(Random.Range(-100,100), 0, Random.Range (-100,100));
		direction.Normalize();
		lethal = false;
		start = renderer.material;
		chaseStart = Time.time + 2.5f;
		chaseEnd = Time.time + 5.5f;
	}
	
	// FixedUpdate is called once per timestep
	void FixedUpdate () {
		// rigidbody.AddForce(direction);
		if (transform.position.y < -5) {
			Destroy(gameObject);
		}
		if (!lethal && transform.position.y <= 1.2) {
			lethal = true;
			t = Time.time;
		}
		if (lethal) {
			if (Time.time > chaseStart && Time.time < chaseEnd) {
				Vector3 towardPlayer = findClosestPlayer() - transform.position;
				towardPlayer.Normalize();
				//rigidbody.velocity = (towardPlayer * 4);
				rigidbody.AddForce(towardPlayer * 20);
			} else {
				rigidbody.AddForce(direction * 20);
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
	
	void Update() {
		if (lethal) {
			float lerp = (Time.time - t) * 0.7f;
			renderer.material.Lerp(start, mat, lerp);
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
