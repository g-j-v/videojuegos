using UnityEngine;
using System.Collections;
using System.Text;

public class playerActions : MonoBehaviour {
	private float strafeDirection;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		strafeDirection = Input.GetAxis ("Horizontal");
		if (strafeDirection < 0) {
			 transform.Translate(0.1f, 0, 0);
		} else if  (strafeDirection > 0) {
			transform.Translate(-0.1f, 0, 0);
		} else {
			transform.Translate(0, 0, 0);
		}
	}
}
