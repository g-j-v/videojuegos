using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TronBrikeManager : MonoBehaviour
{
	private float speed;
	private Rigidbody tronController;
	private Vector3 rotateDirection;
	private static Vector3 leftTurn, rightTurn;
	public GameObject TronTrail, TronTrailCollider;


	// Use this for initialization
	void Start ()
	{
		speed = 300.0f;
		leftTurn = new Vector3(0, -90f, 0);
		rightTurn = new Vector3(0, 90f, 0);
		this.tronController = GetComponent<Rigidbody> ();
		
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
		
		horizontalAxis = Input.GetAxis ("Horizontal");
		
		
		clampHorizontalAxis = horizontalAxis / Mathf.Abs (horizontalAxis);
		
		if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D))) {
			if (clampHorizontalAxis < 0) {
				rotateDirection = leftTurn;
			} else {
				rotateDirection = rightTurn;
			}
			
			Debug.Log(rotateDirection);
			this.tronController.transform.Rotate (rotateDirection, Space.Self);
		}
		
		//print(moveDirection);
		this.tronController.velocity = transform.forward * speed * Time.deltaTime;
		TronTrail.transform.position = transform.position;
		TronTrailCollider.transform.position = transform.position;
	}
	
	void OnParticleCollision(GameObject other) {
    	Debug.Log("Hit!");
	}
}
