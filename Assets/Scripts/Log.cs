using UnityEngine;
using System.Collections;

public class Log : Enemy {

	public Vector3 direction;
	public Material mat;
	public Material dead;
	private Material start;
	public static string[] states = {"spawn", "start", "dying"};
	public string state;
	private float changeTime;
	private bool firstPush;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Cube";
		//float scale = 1.2f - ((float)Random.Range(0,100))/120f;
		//transform.localScale = new Vector3(scale, scale, scale);
		//direction = new Vector3(Random.Range(-100,100), 0, Random.Range (-100,100));
		//direction.
		if (gameObject.transform.position.x > 8)
		{
			direction = new Vector3(-90.0f, 0.0f, 0.0f);
		}
		else if (gameObject.transform.position.x < -8)
		{
			direction = new Vector3(90.0f, 0.0f, 0.0f);
		}
		else if (gameObject.transform.position.z > 8)
		{
			direction = new Vector3(0.0f, 0.0f, -90.0f);
		}
		else
		{
			direction = new Vector3(0.0f, 0.0f, 90.0f);
		}
		
		start = new Material(renderer.material);
		state = Cube.states[0];
		changeTime = Time.time + 0.6f;
		firstPush = true;
	}
	
	// FixedUpdate is called once per timestep
	void FixedUpdate () {
		
		//adding more gravity
		rigidbody.AddForce(0,-200,0);
		
		// rigidbody.AddForce(direction);
		if (transform.position.y < -5) {
			Destroy(gameObject);
		}
		if (state == "start") {
			rigidbody.AddForce(direction * 20);
		}// else if (state == "leave") {
		//	rigidbody.AddForce(direction * 20);
	//	}
		else if (state == "dying") {
			transform.position += Vector3.up * 3;
			if (transform.position.y > 4) Destroy (gameObject);
		} else if (state == "spawn") {
			if (Time.time > changeTime){
				state = "start";
				changeTime += 1f;
			}
		}
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
	
	
	void OnCollisionEnter(Collision col) {
		if (col.relativeVelocity.magnitude > 20 && col.gameObject.tag == "Cube") {
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce(Vector3.up * 400);
		}
	}
}
