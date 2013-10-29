using UnityEngine;
using System.Collections;


//Just in case I want to 
//aCurrentlySelectedUnit.GetComponent<BirdUnitFunctionalityAndStats>().moveCost

public class Move : MonoBehaviour {
	
	//public int lives;
	
	// Use this for initialization
	void Start () {
		gameObject.tag = "Player";
		boom();
		//lives = 3;
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	// Fixed update is called once per timestep
	void FixedUpdate () {
		if (transform.position.y < -1) killMe();
		Vector3 direction = new Vector3(Input.GetAxis("Move X"),0,Input.GetAxis("Move Y"));
		rigidbody.velocity = rigidbody.velocity * 0.95f;
		transform.position += direction * 0.3f;
		//rigidbody.AddForce(direction * 100);
		//transform.rotation = Quaternion.LookRotation(direction);
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cube") {
			boom();
			gameObject.transform.parent.SendMessage("takeDamage");
			//Moved to PlayerSphere
			//if (lives == 0) killMe();
			//lives --;
		}
	}
	
	public void boom(float range = 3f, float magnitude = 400f) {
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag ("Cube");
		foreach (GameObject child in allObjects) {
    		float dist = (transform.position - child.transform.position).magnitude;
    		if (dist < range) {
				Vector3 boom = ((child.rigidbody.position - transform.position).normalized + Vector3.up * 0.5f).normalized;
				child.rigidbody.velocity = Vector3.zero;
				child.GetComponent<Cube>().state = "leave";
				child.rigidbody.AddForce(boom * magnitude);
				boom.y = 0;
				boom.Normalize();
				child.GetComponent<Cube>().direction = boom;
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
	
}
