using UnityEngine;
using System.Collections;
using InControl;

public class InControlManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InputManager.Setup();
	}
	
	// FixedUpdate is called once per physics time step
	void FixedUpdate () {
		InputManager.Update();
	}
}
