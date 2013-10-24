using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	private Vector3 direction;
	private bool lethal;

	// Use this for initialization
	void Start () {
		direction = new Vector3(Random.Range(-100,100), 0, Random.Range (-100,100));
		direction.Normalize();
		lethal = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction * 0.1f;
		// rigidbody.AddForce(direction);
		if (transform.position.y < -5) {
			Destroy(gameObject);
		}
		if (!lethal && transform.position.y <= 1) {
			lethal = true;
		}
	}
}
