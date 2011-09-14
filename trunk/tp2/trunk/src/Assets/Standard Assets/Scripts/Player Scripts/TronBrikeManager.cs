using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TronBrikeManager : MonoBehaviour
{
	private float speed, rotation;
	private Rigidbody tronController;
	private Vector3 moveDirection, rotateDirection;


	// Use this for initialization
	void Start ()
	{
		speed = 50.0f;
		rotation = 90f;
		this.tronController = GetComponent<Rigidbody> ();
		
		moveDirection = Vector3.zero;
		rotateDirection = Vector3.zero;
	}

	// Update is called once per frame
//	void Update ()
	//{

	//}

	void Update ()
	{
		// Defines 90 degree turn
		float clampHorizontalAxis;
		float horizontalAxis;
		Vector3 oldDirection = transform.rotation.eulerAngles;
		
		horizontalAxis = Input.GetAxis ("Horizontal");
		
		
		clampHorizontalAxis = horizontalAxis / Mathf.Abs (horizontalAxis);
		
		if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) && horizontalAxis != 0) {
			rotateDirection = new Vector3 (0, clampHorizontalAxis, 0);
			
			this.tronController.transform.Rotate (rotateDirection * rotation);
			Debug.Log (transform == this.tronController.transform);
		}
		
		//print(moveDirection);
		this.tronController.velocity = transform.forward * speed * Time.deltaTime;
		
	}
}
