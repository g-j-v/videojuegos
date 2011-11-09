using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	private float speed;
	private float rocketsDirection;
	protected bool paused;
	
	
	// Use this for initialization
	void Start () {
		speed = 100.0f;
		rocketsDirection = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate ()
	{
		Vector3 moveDirection;
		
		if (!paused)
		{
			moveDirection = new Vector3(rocketsDirection,0,0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			rigidbody.velocity = moveDirection * Time.deltaTime;
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (!paused)
		{
			if (other.tag == "WallDestroyer")
			{
				Destroy(gameObject);
			}
			else if (other.tag == "Player")
			{
				playerActions pa = other.GetComponent<playerActions>();
				pa.ExplodeAndDestroy();
				Destroy(gameObject); // Destruye el rocket
			}
		}
	}
	
	public void OnPauseGame()
	{
		this.paused = true;
	}
	
	public void OnResumeGame()
	{
		this.paused = false;
	}
}