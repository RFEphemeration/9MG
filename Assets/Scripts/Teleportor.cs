using UnityEngine;
using System.Collections;

public class Teleportor : MonoBehaviour {
	
	private static float RANGE = 7.0f; 
	private static float RECHARGE = 1.0f;
	private static float RATE = 1.5f;

	float startTime;
	Vector3 direction;
	bool startedCounting;
	
	//Right trigger charge
	private double charge;
	
	public GameObject maxRangeSphereType;
	private GameObject maxRangeSphere;
	
	public Teleportor()
	{
		startTime = Time.time;
		startedCounting = false;
		direction = Vector3.zero;
	}
	
	// Update is called once per frame
	public Vector3 GetTeleport () {
		
		//RIGHT TRIGGER CHARGE CONTROL SCHEME
		direction.x = Input.GetAxis("Aim X");
		direction.z = Input.GetAxis("Aim Y");
		
		charge = Input.GetAxis("Charge 1");
		
		bool fire2 = (Mathf.Abs(direction.x) >= 0.1 || Mathf.Abs(direction.z) >= 0.1);
		bool flag = false;
		
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
		float range = (Time.time - startTime) * RANGE * RATE;
		range = Mathf.Min(range, RANGE);
		
		if (startedCounting && charge > 0.5)
		{
			if (charge > 0.5)
			{
				//growing the spehere
				direction.Normalize();		
				maxRangeSphere.transform.localScale = new Vector3(2 * range, 1, 2 * range);
				maxRangeSphere.transform.rotation = Quaternion.identity;
			}
			if (fire2 || charge <= 0.5)
			{
				direction *= range;
				flag = true;
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
				flag = true;
				// else we want to cancel teleport
				if (fire2) startTime = Time.time + RECHARGE;
				else startTime = Time.time;
				startedCounting = false;
				direction = Vector3.zero;
				Destroy(maxRangeSphere);
		}
		if (flag) {
			return direction;
		} else {
			return new Vector3(0, 9001, 0);
		}
	}
}
