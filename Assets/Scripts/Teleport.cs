using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	
	float startTime;
	float range; 
	Vector3 direction;
	bool startedCounting;
	
	void Start()
	{
		startedCounting = false;
		range = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if ((/*Input.GetAxis("Joystick 10th") > 0  ||*/ Input.GetKeyDown(KeyCode.Space)) && !startedCounting)
		{
			startedCounting = true;
			startTime = Time.time;	
		}
		if (/*Input.GetAxis("Joystick 10th") <= 0 ||*/ Input.GetKeyUp(KeyCode.Space))
		{
			range = Time.time - startTime;
			range = Mathf.Min(range * 3.0f, 5.0f);
			direction = (new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y")));
			direction = (direction - transform.position).normalized;
			if (direction.y != 0f) direction.y = 0f;
			direction *= range;
			teleportDirection(direction);
			startedCounting = false;
		}
		
	}
	
	public void teleportDirection(Vector3 d)
	{
		transform.position += d;
		gameObject.GetComponent<Move>().boom();
	}
}
