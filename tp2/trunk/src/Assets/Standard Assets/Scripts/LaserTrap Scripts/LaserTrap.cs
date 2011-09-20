using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour {
	public float height = 3.2f;
	public float speed = 2.0f;
	public float timingOffset = 0.0f;
	public float laserWidth = 12.0f;
	public float damage = 1f;
	public GameObject hitEffect;
	protected bool paused;
	
	private float lastHitTime = 0.0f;
	private const float laserSpeed = 1200f;

	public void Start ()
	{
		GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * laserWidth);
		height = 3.2f;
	}

	void Update ()
	{
		
	}
	
	void FixedUpdate()
	{
	}
	
	

	void OnTriggerEnter(Collider other) 
	{
		Vector3 pos;
		Debug.Log("Crashed: " + other.name);
		//pos = GetComponent<Transform>().position;
		//pos.y += laserWidth/2;
		Instantiate(hitEffect, other.transform.position, Quaternion.identity);

		if (other.tag == "Enemy")
		{
			
		} else if (other.tag == "WallDestroyer")
		{
			Destroy(gameObject);
		}
	}
}