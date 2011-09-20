using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TronBrikeManager : MonoBehaviour
{
	private float speed;
	private Rigidbody tronController;
	private Vector3 rotateDirection;
	private static Vector3 leftTurn, rightTurn;
	public GameObject TronTrail, TronTrailCollider, Detonation;
	private GameObject bike;


	// Use this for initialization
	void Start ()
	{
		speed = 300.0f;
		leftTurn = new Vector3(0, -90f, 0);
		rightTurn = new Vector3(0, 90f, 0);
		this.tronController = GetComponent<Rigidbody> ();
		this.bike = gameObject;
		
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
		KeyCode left, right;
		
		if (!TronGameManager.finishGame) {
			if (bike.name == "TronBike2") {
				horizontalAxis = Input.GetAxis ("HorizontalPlayer2");
				left = KeyCode.LeftArrow;
				right = KeyCode.RightArrow;
			} else {
				horizontalAxis = Input.GetAxis ("Horizontal");
				left = KeyCode.A;
				right = KeyCode.D;
			}
			
			
			clampHorizontalAxis = horizontalAxis / Mathf.Abs (horizontalAxis);
			
			
			if ((Input.GetKeyDown (left) || Input.GetKeyDown (right))) {
				if (clampHorizontalAxis < 0) {
					rotateDirection = leftTurn;
				} else {
					rotateDirection = rightTurn;
				}
				
				Debug.Log(rotateDirection);
				this.tronController.transform.Rotate (rotateDirection, Space.Self);
				this.tronController.MovePosition(this.tronController.transform.position + this.tronController.transform.forward*1.1f);
			}
			
			//print(moveDirection);
			this.tronController.velocity = transform.forward * speed * Time.deltaTime;
			TronTrail.transform.position = transform.position;
			TronTrailCollider.transform.position = transform.position;
		} else {
			this.tronController.velocity = Vector3.zero;
		}
	}
	
	void OnParticleCollision(GameObject other) {
		
    	if (bike.name == "TronBike2" && other.name == "TrailCollider") {
			TronGameManager.playerOneWon();
			ExplodeAndDestroy();
			return;
		}
		
		if (bike.name == "TronBike" && other.name == "TrailCollider2") {
			TronGameManager.playerTwoWon();
			ExplodeAndDestroy();
			return;
		}
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<LaserTrap>()) {
			if (bike.name == "TronBike") {
				TronGameManager.playerTwoWon();
			} else {
				TronGameManager.playerOneWon();
			}
			ExplodeAndDestroy();
		} else {
			if (other.name == "TronBike" || other.name == "TronBike2") {
				ExplodeAndDestroy();
				TronGameManager.evenGame();
			}
		}
	}
	
	public void ExplodeAndDestroy()
	{	
		Detonation.transform.position = this.transform.position;
		Detonator d = Detonation.GetComponent<Detonator>();
		d.Explode();
		Debug.Log("Player: " + bike.name + " lost!");
		Destroy(TronTrail);
		Destroy(TronTrailCollider);
		Destroy(this.bike); // Destruye al player porque pierde
	}

}
