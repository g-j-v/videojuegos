using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour {
	public float height = 3.2f;
	public float speed = 2.0f;
	public float timingOffset = 0.0f;
	public float laserWidth = 12.0f;
	public float damage = 1f;
	public GameObject hitEffect;
	
	private float lastHitTime = 0.0f;
	private const float laserSpeed = 1200f;

	public void Start ()
	{
		GetComponent<LineRenderer>().SetPosition(1, Vector3.up * laserWidth);
		height = 3.2f;
	}

	void Update ()
	{
		
	}
	
	void FixedUpdate()
	{
		Vector3 moveDirection;
		moveDirection = Vector3.up;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= laserSpeed;
		rigidbody.velocity = moveDirection * Time.deltaTime;
	}
	
	

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("laser touch");
		Vector3 pos;
		
		if (Time.time > lastHitTime + 0.25)
		{
			if ( other.tag == "Enemy")
			{
				pos = GetComponent<Transform>().position;
				pos.y += laserWidth/2;
				Instantiate(hitEffect, pos, Quaternion.identity);
				other.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				lastHitTime = Time.time;
				Debug.Log("destruyendome!");
				//GetComponent(LineRenderer).SetVertexCount(0);
				//var go = GameObject.FindObjectByType<Laser>();
				Destroy(gameObject);
			}
		}
	}
}