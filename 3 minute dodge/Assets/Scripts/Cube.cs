using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public Vector3 direction;
	public bool lethal;
	public Material mat;
	private Material start;
	private float t;

	// Use this for initialization
	void Start () {
		direction = new Vector3(Random.Range(-100,100), 0, Random.Range (-100,100));
		direction.Normalize();
		lethal = false;
		start = renderer.material;
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
			rigidbody.AddForce(direction * 20);
		}
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
