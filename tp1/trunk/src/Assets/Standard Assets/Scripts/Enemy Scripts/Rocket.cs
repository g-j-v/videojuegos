using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	private float speed;
	private Vector3 oldVelocity;
	
	// Use this for initialization
	void Start () {
		speed = 100.0f;
		Debug.Log("rocket created\n");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(0, InvadersGameData.rocketsDirection, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		oldVelocity = rigidbody.velocity = moveDirection * Time.deltaTime;
	}
}
