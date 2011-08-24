using UnityEngine;
using System.Collections;
using System.Text;

[RequireComponent(typeof(CharacterController))]
public class playerActions : MonoBehaviour {
	private float strafeDirection;
	private CharacterController playerController;
	private float speed = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
	private GameObject player;
	private float nextFire = 0.0F;
	protected bool paused;
	public float fireRate = 0.5F;
	public LaserTrap ls;
	public GameObject Detonation;
	
	
	// Use this for initialization
	void Start () {
		this.playerController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = transform.position;
		
		if (!paused)
		{
			pos.y += ls.laserWidth;
				
			if (Input.GetButton("Fire1") && Time.time > nextFire) 
			{
	            nextFire = Time.time + fireRate;
	            Instantiate(ls, pos, Quaternion.identity);
	        }
		}
	}
	
	void FixedUpdate()
	{
		if (!paused)
		{
			moveDirection = new Vector3(-1.0f*Input.GetAxis("Horizontal"), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			this.playerController.Move(moveDirection * Time.deltaTime);
		}
	}
	
	
	public void ExplodeAndDestroy()
	{
		this.player = gameObject;
		
		if (this.player != null)
		{
			Detonation.transform.position = this.transform.position;
			Detonator d = Detonation.GetComponent<Detonator>();
			d.Explode();
			Debug.Log("Perdiste Capo!");
			InvadersGameData.gameLost = true;
			Destroy(this.player); // Destruye al player porque pierde
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