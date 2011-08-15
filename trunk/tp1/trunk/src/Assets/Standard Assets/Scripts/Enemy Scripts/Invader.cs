using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	private int invaderID;
	private float direction;
	private float speed;
	
	// Use this for initialization
	void Start () 
	{
		direction = 20.0f;
		speed = 2.0f;
		Debug.Log("invader created\n");
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(direction, 0, 0);
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
}
