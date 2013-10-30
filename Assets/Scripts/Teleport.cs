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
	
	void Start()
	{
		startTime = Time.time;
		startedCounting = false;
		direction = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		direction.x = Input.GetAxis("Aim X");
		direction.z = Input.GetAxis("Aim Y");
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
		}
		
	}
	
	public void teleportDirection(Vector3 d)
	{
		transform.position += d;
		gameObject.GetComponent<Move>().boom();
	}
}
