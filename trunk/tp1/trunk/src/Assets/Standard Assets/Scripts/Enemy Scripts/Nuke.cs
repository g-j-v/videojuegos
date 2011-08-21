using UnityEngine;
using System.Collections;

public class Nuke : MonoBehaviour {
	public GameObject AtomicDetonation;
	private float speed;
	private float rocketsDirection;
	
	// Use this for initialization
	void Start () {
		speed = 200.0f;
		rocketsDirection = -2.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() 
	{
		Vector3 moveDirection;
		moveDirection = new Vector3(0,rocketsDirection,0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		rigidbody.velocity = moveDirection * Time.deltaTime;	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		AtomicDetonation.transform.position = other.transform.position;
		Detonator d = AtomicDetonation.GetComponent<Detonator>();
		d.Explode();
		Destroy(other.gameObject); // Destruye al player
		Destroy(gameObject); // Destruye el rocket
	}
}
