using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Fixed update is called once per timestep
	void FixedUpdate () {
		
		//adding more gravity
		rigidbody.AddForce(0,-50,0);
		
		
		Vector3 direction = new Vector3(Input.GetAxis("Move X"),0,Input.GetAxis("Move Y"));
		rigidbody.velocity = rigidbody.velocity * 0.90f;
		
		float dot = Vector3.Dot(direction, rigidbody.velocity);
		if (dot < 0) {
			rigidbody.AddForce(direction * 40);
			transform.position += direction * 0.16f;
		} else {
			transform.position += direction * 0.31f;
			rigidbody.AddForce(direction * 15);
		}
		//if rigidbody.velocity.sqrMagnitude > 
		//transform.rotation = Quaternion.LookRotation(direction);
	}
	
}
