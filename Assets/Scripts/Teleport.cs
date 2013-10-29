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
		
		if (Input.GetAxis("Charge 1") > 0.001 && !startedCounting && startTime < Time.time)
		{
			startedCounting = true;
			startTime = Time.time;
			reticle = (GameObject)Instantiate(reticleType, Vector3.zero, Quaternion.identity);
			reticle.transform.parent = gameObject.transform;
			reticle.transform.position = gameObject.transform.position + Vector3.zero;
		}
		
		if (startedCounting) {
			direction.x = Input.GetAxis("Aim X");
			direction.z = Input.GetAxis("Aim Y");
			range = (Time.time - startTime) * RANGE * RATE;
			range = Mathf.Min(range, RANGE);
			reticle.transform.position = gameObject.transform.position + direction.normalized * range;
			if (Input.GetAxis("Charge 1") <= 0.001) {
				if (direction.sqrMagnitude > 0.5) {
					direction.Normalize();
					direction *= range;
					teleportDirection(direction);
				} // else we aren't holding a direction and shouldn't be able to teleport in place
				startedCounting = false;
				startTime = Time.time + RECHARGE;
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
