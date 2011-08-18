using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	private float speed;
	private float rocketsDirection;
	
	// Use this for initialization
	void Start () {
		speed = 100.0f;
		rocketsDirection = 2.0f;
		Debug.Log("rocket created\n");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(rocketsDirection,0,0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		rigidbody.velocity = moveDirection * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other)
	{
		GameObject player;
		player = other.gameObject;
		
		if (player != null)
		{
			Destroy(player);
			Destroy(this.gameObject); // Destruye el Rocket
		}
	}
}
