using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	
	private static float RANGE = 7.0f; 
	private static float RECHARGE = 1.0f;
	private static float RATE = 1.5f;
	
	float range;
	float startTime;
	Vector3 direction;
	bool startedCounting;
	
	public GameObject reticleType;
	private GameObject reticle;
	
	
	//Right trigger charge
	private double charge;
	
	public GameObject maxRangeSphereType;
	private GameObject maxRangeSphere;
	
	void Start()
	{
		startTime = Time.time;
		startedCounting = false;
		direction = Vector3.zero;
	}
	
	// Update is called once per frame
	void TeleportUpdate () {
		
		//RIGHT TRIGGER CHARGE CONTROL SCHEME
		direction.x = Input.GetAxis("Aim X");
		direction.z = Input.GetAxis("Aim Y");
		
		charge = Input.GetAxis("Charge 1");
		
		bool fire2 = (Mathf.Abs(direction.x) >= 0.1 || Mathf.Abs(direction.z) >= 0.1);
		
		if (charge >= 0.5 && !startedCounting && startTime < Time.time)
		{
			//createTheSphere
			direction.Normalize();
			startedCounting = true;
			startTime = Time.time;
			maxRangeSphere = (GameObject) Instantiate(maxRangeSphereType, Vector3.zero, Quaternion.identity);
			maxRangeSphere.transform.parent = gameObject.transform;
			
			//if (maxRangeSphere.transform.rotation != Quaternion.Euler(0, 0, 0)) {
         	//	maxRangeSphere.transform.rotation = Quaternion.Euler(0, 0, 0);
     		//}
			
			maxRangeSphere.transform.position = gameObject.transform.position + Vector3.zero;
			//maxRangeSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if (startedCounting && charge > 0.5)
		{
			if (charge > 0.5)
			{
				//growing the spehere
				direction.Normalize();
				range = (Time.time - startTime) * RANGE * RATE;
				range = Mathf.Min(range, RANGE);
				maxRangeSphere.transform.localScale = new Vector3(2 * range, 1, 2 * range);
				maxRangeSphere.transform.rotation = Quaternion.identity;
			}
			if (fire2 || charge <= 0.5)
			{
				direction *= range;
				teleportDirection(direction);
				// else we want to cancel teleport
				if (fire2) startTime = Time.time + RECHARGE;
				else startTime = Time.time;
				startedCounting = false;
				direction = Vector3.zero;
				Destroy(maxRangeSphere);
			}
		}
		else if (startedCounting && charge <= 0.5)
		{
				direction *= range;
				teleportDirection(direction);
				// else we want to cancel teleport
				if (fire2) startTime = Time.time + RECHARGE;
				else startTime = Time.time;
				startedCounting = false;
				direction = Vector3.zero;
				Destroy(maxRangeSphere);
		}
	
		
		//ORIGINAL TELEPORT CONTROL SCHEME
		/*direction.x = Input.GetAxis("Aim X");
		//direction.z = Input.GetAxis("Aim Y");
		bool fire = Input.GetAxis("Charge 1") >= 0.02;
		if ((direction.sqrMagnitude > 0.5 || fire) && !startedCounting && startTime < Time.time)
		{
			direction.Normalize();
			startedCounting = true;
			startTime = Time.time;
			reticle = (GameObject)Instantiate(reticleType, Vector3.zero, Quaternion.identity);
			reticle.transform.parent = gameObject.transform;
			reticle.transform.position = gameObject.transform.position + Vector3.zero;
		}
		
		if (startedCounting) {
			if (direction.sqrMagnitude > 0.5) {
				direction.Normalize();
				range = (Time.time - startTime) * RANGE * RATE;
				range = Mathf.Min(range, RANGE);
				reticle.transform.position = gameObject.transform.position + direction.normalized * range;
			}
			
			if (fire) {
				direction *= range;
				teleportDirection(direction);
			}
			if (direction.sqrMagnitude < 0.01 || fire) {
				// else we want to cancel teleport
				if (fire) startTime = Time.time + RECHARGE;
				else startTime = Time.time;
				startedCounting = false;
				direction = Vector3.zero;
				Destroy(reticle);
			}
		}*/
		
	}
	
	public Vector3 teleportDirection(Vector3 d)
	{
		rigidbody.velocity = Vector3.zero;
		transform.position += d;
		gameObject.GetComponent<Move>().boom();
	
		return d;
	}
}
