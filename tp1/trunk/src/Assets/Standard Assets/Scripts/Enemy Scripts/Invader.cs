using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	private int invaderID;
	private float speed;
	private Vector3 oldVelocity;
	public Rocket rocketPrefab;
	public Nuke nukePrefab;
	public GameObject detonation; 
	
	// Use this for initialization
	void Start () 
	{
		speed = 80.0f;
		Debug.Log("invader created\n");
	}
	
	// Update is called once per frame
	void Update ()
	{
		float fireConstraint = Random.value;
		
		if (fireConstraint < 0.0025f && InvadersGameData.canFire(this.invaderID))
		{
			Vector3 rPosition = transform.position;
			rPosition.x -= 1.5f;
			// logica de disparo
			
			if (fireConstraint < 0.00025f)
			{
				Instantiate(nukePrefab, rPosition, Quaternion.identity);
			}
			else
			{
				Instantiate(rocketPrefab, rPosition, Quaternion.Euler(0,0,270));
			}
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
		if (InvadersGameData.directionChanged == false && other.tag == "Wall")
		{
        	InvadersGameData.invadersDirection *= -1.0f;
			InvadersGameData.directionChanged = true;
			InvadersGameData.descendInvaders();
		}
		else if (other.tag == "Laser")
		{
			InvadersGameData.notifyDecease(this.invaderID);
			ExplodeAndDestroy();
		} else if (other.tag == "Player")
		{
			playerActions pa = other.GetComponent<playerActions>();
			pa.ExplodeAndDestroy();
			ExplodeAndDestroy();
		}
    }
	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Wall") 
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
	
	private void ExplodeAndDestroy()
	{
		detonation.transform.position = this.rigidbody.position;
		Detonator d = detonation.GetComponent<Detonator>();
		d.Explode();
		Destroy(gameObject); // Destruye el invader
	}
}
