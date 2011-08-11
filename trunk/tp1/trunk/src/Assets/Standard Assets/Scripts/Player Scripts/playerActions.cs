using UnityEngine;
using System.Collections;
using System.Text;

[RequireComponent(typeof(CharacterController))]
public class playerActions : MonoBehaviour {
	private float strafeDirection;
	private CharacterController playerController;
	private float speed = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		this.playerController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		moveDirection = new Vector3(-1.0f*Input.GetAxis("Horizontal"), 0, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		this.playerController.Move(moveDirection * Time.deltaTime);
	}
}
