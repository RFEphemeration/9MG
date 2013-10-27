using UnityEngine;
using System.Collections;


//Just in case I want to 
//aCurrentlySelectedUnit.GetComponent<BirdUnitFunctionalityAndStats>().moveCost

public class Move : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.tag = "Player";
		boom();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * 0.3f;
		transform.position += direction;
		//rigidbody.AddForce(direction * 100);
		//transform.rotation = Quaternion.LookRotation(direction);
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cube") { 
			
			boom();
			
			
			//Destroy
			Destroy(gameObject);
			
			//Pause and give option to reset.
			//tell the pause menu to pause
			PauseMenuGUI.gameOver = true;
			
		}
	}
	
	void boom(float range = 5f, float magnitude = 400f) {
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag ("Cube");
			foreach (GameObject child in allObjects) {
    			float dist = (transform.position - child.transform.position).magnitude;
    			if (dist < range) {
					Vector3 boom = ((child.rigidbody.position - transform.position).normalized + Vector3.up * 0.1f).normalized;
					child.rigidbody.velocity = Vector3.zero;
					child.GetComponent<Cube>().direction = boom;
					child.rigidbody.AddForce(boom * magnitude);
				}
    		}
	}
	
}
