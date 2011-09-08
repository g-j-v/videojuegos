using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TronBrikeManager : MonoBehaviour {
	private float speed, rotation;
	private Rigidbody tronController;
	private Vector3 moveDirection;
	private bool movingHorizontal, movingVertical;
	
	// Use this for initialization
	void Start () {
		speed = 50.0F;
		rotation = 90f;
		this.tronController = GetComponent<Rigidbody>();
		movingVertical = false;
		movingHorizontal = true;
		moveDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		// Defines 90 degree turn
		float clampHorizontalAxis, clampVerticalAxis;
		float horizontalAxis, verticalAxis;
		Vector3 oldDirection = moveDirection;
		
		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");
		clampHorizontalAxis = horizontalAxis / Mathf.Abs(horizontalAxis);
		clampVerticalAxis = verticalAxis / Mathf.Abs(verticalAxis);
		
		if (horizontalAxis != 0 && verticalAxis != 0) {
			/* do nothing, continue moving */
		} else if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && horizontalAxis != 0) {
			moveDirection = new Vector3(clampHorizontalAxis, 0, 0);
			Debug.Log("horizontal old: " + oldDirection + " new: " + moveDirection);
			transform.rotation = Quaternion.FromToRotation(oldDirection,moveDirection);
		} else if ((Input.GetKeyDown("w") || Input.GetKeyDown("s")) && verticalAxis != 0){
			moveDirection = new Vector3(0, 0, clampVerticalAxis);
			Debug.Log("vertical old: " + oldDirection + " new: " + moveDirection);
			transform.rotation = Quaternion.FromToRotation(oldDirection,moveDirection);
		} else { /* do nothing, continue moving*/}
				
		//print(moveDirection);
		this.tronController.velocity = moveDirection * speed * Time.deltaTime;
	}
}
