using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	private int invaderID;
	private float speed;
	private Vector3 oldVelocity;
	
	// Use this for initialization
	void Start () 
	{
		speed = 100.0f;
		Debug.Log("invader created\n");
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(InvadersGameData.invadersDirection, 0, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		oldVelocity = rigidbody.velocity = moveDirection * Time.deltaTime;
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
        Debug.Log("Collide!");
		if (InvadersGameData.directionChanged == false)
		{
			Debug.Log("changing direction!");
        	InvadersGameData.invadersDirection *= -1.0f;
			InvadersGameData.directionChanged = true;
			InvadersGameData.descendInvaders();
		}
    }
	
	void OnTriggerExit(Collider other) 
	{
		Debug.Log("Collide Exit!");
        InvadersGameData.directionChanged = false;
		rigidbody.velocity = oldVelocity;
	}
	
	public void descend(float distance)
	{
		Vector3 moveDirection = transform.position;
		moveDirection.y -= distance;
		transform.position = moveDirection;
	}
}
