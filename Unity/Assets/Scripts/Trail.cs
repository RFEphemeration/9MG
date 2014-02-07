using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {

	float spawnTime;
	float delay = 0.05f;
	float lifeTime = 0.1f;

	bool active;
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (!active && Time.time - spawnTime > delay) {
			active = true;
			gameObject.renderer.enabled = true;
		}
		if (Time.time - spawnTime > lifeTime) {
			Destroy (gameObject);
		}
	}

	void setScale(float magnitude) {
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * magnitude);
	}

	void setRotation(float y) {
		transform.RotateAround(transform.position, Vector3.up, y);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cube" || col.gameObject.tag == "Log") { // find a better way of grouping enemies
			col.gameObject.SendMessage("killMe");
		}

	}
}
