using UnityEngine;
using System.Collections;
using InControl;

public class Teleporter : MonoBehaviour {
	
	private static float RANGE = 7.0f; 
	private static float RECHARGE = 1.0f;
	private static float RATE = 1.5f;
	
	float startTime;
	Vector3 direction;
	public bool startedCounting;
	
	//Right trigger charge
	private double charge;
	
	public GameObject maxRangeSphereType;
	private GameObject maxRangeSphere;

	public GameObject trail;
	
	private int id = 1;
	
	void Start() {
		startTime = Time.time;
		startedCounting = false;
		direction = Vector3.zero;
		maxRangeSphere = (GameObject) MonoBehaviour.Instantiate(maxRangeSphereType, Vector3.zero, Quaternion.identity);
		maxRangeSphere.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		maxRangeSphere.transform.parent = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update() {
		maxRangeSphere.transform.position = gameObject.transform.position;
		if (startedCounting) {
			float range = (Time.time - startTime) * RANGE * RATE;
			range = Mathf.Min(range, RANGE);
			//growing the spehere	
			maxRangeSphere.transform.localScale = new Vector3(2 * range, 1, 2 * range);
			maxRangeSphere.transform.rotation = Quaternion.identity;
		}
	}
	
	void FixedUpdate() {
		//RIGHT TRIGGER CHARGE CONTROL SCHEME
		direction.x = InputManager.Devices[id].RightStickX;
		direction.z = InputManager.Devices[id].RightStickY;
		
		charge = InputManager.Devices[id].RightTrigger;
		
		bool fire = (Mathf.Abs(direction.x) >= 0.2 || Mathf.Abs(direction.z) >= 0.2);
		
		if (charge >= 0.5 && !startedCounting && startTime < Time.time) {
			//createTheSphere
			startedCounting = true;
			startTime = Time.time;
		}
		
		
		
		if (startedCounting && (fire || charge <= 0.5))
		{
			Vector3 startPosition, endPosition;
			float range = (Time.time - startTime) * RANGE * RATE;
			range = Mathf.Min(range, RANGE);
			direction.Normalize();	
			direction *= range;
			startTime = Time.time + RECHARGE;
			startedCounting = false;
			maxRangeSphere.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			startPosition = gameObject.transform.position;
			endPosition = startPosition + direction;
			gameObject.transform.position += direction;
			GameObject theTrail = (GameObject) Instantiate(trail, (startPosition + endPosition)/2, Quaternion.identity);
			theTrail.SendMessage("setScale", (startPosition-endPosition).magnitude);
			Vector3 forward = new Vector3(0,0,1);
			Vector3 right = Vector3.Cross(Vector3.up, forward);
			Vector3 dir = endPosition - startPosition;
			float angle = Vector3.Angle(dir, forward);
			float sign = (Vector3.Dot(dir, right) > 0.0f) ? 1.0f: -1.0f;
			float finalAngle = sign * angle;
			theTrail.SendMessage("setRotation", finalAngle);
			
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.SendMessage("boomDefault");
		}
	}
	
	void setTeleporterID(int pID)
	{
		id = pID;
	}
}
