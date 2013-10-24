using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.3f,0,Input.GetAxis("Vertical") * 0.3f);
	}
	
	void OnCollisionEnter() {
		Destroy(gameObject);
	}
}
