using UnityEngine;
using System.Collections;
using InControl;

public class InControlManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InputManager.Setup();
		InputManager.OnDeviceAttached += inputDevice => Debug.Log( "Attached: " + inputDevice.Name );
		InputManager.OnDeviceDetached += inputDevice => Debug.Log( "Detached: " + inputDevice.Name );
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.Update();
			foreach (var d in InputManager.Devices ) {
				Debug.Log(d.Name);
			}
	}
}
