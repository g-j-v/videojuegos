using UnityEngine;
using System.Collections;

public class raceManager : MonoBehaviour {
	public GameObject roadCreator;
	public Texture2D redLight, yellowLight, greenLight;
	public GameObject carGO;
	private static int checkPointsDone, checkPointsQty;
	private bool finishOnTime, start;
	private float timeLimit = 60.0f;
	
	// Use this for initialization
	void Start () {
		roadCreator.GetComponent<RoadCreator>().Generate();
		checkPointsDone = 0;
		checkPointsQty = roadCreator.transform.childCount;
		start = false;
		Debug.Log(checkPointsQty);
	}
	
	void OnGUI() {
		if (Time.time < 2.0f) {
        	GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), redLight);
		} else if (Time.time < 4.0f) {
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), yellowLight);
			
		} else if (Time.time < 6.0f) {
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), greenLight);
			start = true;
			carGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		
		GUI.Label(new Rect(Screen.width - Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.1f), "Time Left: " + timeLimit);
    }
	
	// Update is called once per frame
	void Update () {
		if (start) {
			if (timeLimit <= 0) {
				Debug.Log("You've Lost!");
				finishOnTime = false;
			}
			
			// Si paso por todos los checkpoints termino a tiempo la carrera (falta lo del tiempo)
			if (checkPointsDone == checkPointsQty) {
				finishOnTime = true;
				Debug.Log("You've Won!");
			} else {
				timeLimit -= 1.0f * Time.deltaTime;
			}
		} else {
			if (Time.time > 0.7f) {
				carGO.GetComponent<Rigidbody>().constraints = carGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
		}
	}
	
	public static void recieveCheckPoint(int chkidx) {
		if (chkidx - checkPointsDone == 1) {
			checkPointsDone++;
			Debug.Log("checkpoint! " + checkPointsDone);
		} else {
			Debug.Log("la pifie");
		}
	}
}
