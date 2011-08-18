using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	private int invaderID;
	private float speed;
	private Vector3 oldVelocity;
	public Rocket rocketPrefab;
	
	// Use this for initialization
	void Start () 
	{
		speed = 100.0f;
		Debug.Log("invader created\n");
	}
	
	// Update is called once per frame
	void Update ()
	{
		float fireConstraint = Random.value;
		
		if (fireConstraint < 0.00025f && InvadersGameData.canFire(this.invaderID))
		{
			Debug.Log("Disparando!!!");
			Vector3 rPosition = transform.position;
			rPosition.x -= 1.5f;
			// logica de disparo
			Instantiate(rocketPrefab, rPosition, Quaternion.Euler(0,0,270));
		}
			
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(InvadersGameData.invadersDirection, 0, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		rigidbody.velocity = moveDirection * Time.deltaTime;
	}
	
	// ID del invader
	public int InvaderID 
	{
		get
		{
			return InvaderID;
		}
		set
		{
			invaderID = value;
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (InvadersGameData.directionChanged == false && other.name == "HorizontalInvisibleWall")
		{
        	InvadersGameData.invadersDirection *= -1.0f;
			InvadersGameData.directionChanged = true;
			InvadersGameData.descendInvaders();
		}
    }
	
	void OnTriggerExit(Collider other) 
	{
		if (other.name == "HorizontalInvisibleWall")
		{
	        InvadersGameData.directionChanged = false;
		}
	}
	
	public void descend(float distance)
	{
		Vector3 moveDirection = transform.position;
		moveDirection.y -= distance;
		transform.position = moveDirection;
	}
}
