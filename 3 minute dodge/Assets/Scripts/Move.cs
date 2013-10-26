using UnityEngine;
using System.Collections;


//Just in case I want to 
//aCurrentlySelectedUnit.GetComponent<BirdUnitFunctionalityAndStats>().moveCost

public class Move : MonoBehaviour {
	public GameObject killer;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.3f,0,Input.GetAxis("Vertical") * 0.3f);
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == killer.tag) { 
			//Destroy
			Destroy(gameObject);
			
			//Boom
			GameObject[] allObjects = GameObject.FindGameObjectsWithTag (killer.tag);
			foreach (GameObject child in allObjects) {
    			float dist = (transform.position - child.transform.position).magnitude;
    			if (child.tag == killer.tag && dist < 5) {
					Vector3 boom = ((child.rigidbody.position - transform.position).normalized + Vector3.up * 0.5f).normalized;
					child.rigidbody.velocity = Vector3.zero;
					child.GetComponent<Cube>().direction = Vector3.zero;
					child.rigidbody.AddForce(boom * 400f);
				}
    		}
			
			//Pause and give option to reset.
			//tell the pause menu to pause
			PauseMenuGUI.gameOver = true;
			
		}
	}
	
}
