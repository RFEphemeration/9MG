using UnityEngine;
using System.Collections;

public class Teleportor : MonoBehaviour {
	
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
		direction.x = Input.GetAxis("Aim X");
		direction.z = Input.GetAxis("Aim Y");
		
		charge = Input.GetAxis("Charge 1");
		
		bool fire = (Mathf.Abs(direction.x) >= 0.2 || Mathf.Abs(direction.z) >= 0.2);
		
		if (charge >= 0.5 && !startedCounting && startTime < Time.time) {
			//createTheSphere
			startedCounting = true;
			startTime = Time.time;
		}
		
		
		
		if (startedCounting && (fire || charge <= 0.5))
		{
			float range = (Time.time - startTime) * RANGE * RATE;
			range = Mathf.Min(range, RANGE);
			direction.Normalize();	
			direction *= range;
			startTime = Time.time + RECHARGE;
			startedCounting = false;
			maxRangeSphere.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			gameObject.transform.position += direction;
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.SendMessage("boom");
		}
	}
}
