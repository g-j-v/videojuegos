using UnityEngine;
using System.Collections;

public class raceManager : MonoBehaviour {
	public GUISkin gSkin;
	public GameObject roadCreator, firstCamera, thirdCamera;
	public Texture2D redLight, yellowLight, greenLight;
	public GameObject carGO;
	private static int checkPointsDone, checkPointsQty;
	private bool finishOnTime, start, end;
	private float timeLimit = 60.0f;
	float resetTime;
	
	// Use this for initialization
	void Start () {
		
		
		roadCreator.GetComponent<RoadCreator>().Generate();
		resetTime=0f;
		
		checkPointsDone = 0;
		checkPointsQty = roadCreator.transform.childCount;
		start = end = false;
		timeLimit = SceneParameters.time;
		timeLimit=SceneParameters.time;
		Debug.Log(checkPointsQty);
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
		if (Time.time < 2.0f) {
        	GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), redLight);
		} else if (Time.time < 4.0f) {
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), yellowLight);
			
		} else if (Time.time < 6.0f) {
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), greenLight);
			start = true;
			carGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		
		if (timeLimit >= 0) {
			GUI.Label(new Rect(Screen.width - Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.2f, Screen.height * 0.1f), "Time Left: " + timeLimit);
		}
		
		if(end){
			if (resetTime == 0) {
				resetTime = Time.time;
			}
			
			if (finishOnTime) {
				GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.20f), "You made it!", "mainMenuTitle");
			} else if (!finishOnTime){
				GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.20f), "You didn't make it!", "mainMenuTitle");
			}
			
			if (Time.time > resetTime + 5) {
				Application.LoadLevel("GUIScene");
			}
		
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (start) {
			if (Input.GetKeyDown(KeyCode.F)) {
				thirdCamera.GetComponent<Camera>().enabled = false;
				firstCamera.GetComponent<Camera>().enabled = true;
			}
			
			if (Input.GetKeyDown(KeyCode.T)) {
				thirdCamera.GetComponent<Camera>().enabled = true;
				firstCamera.GetComponent<Camera>().enabled = false;
			}
			
			if (timeLimit <= 0) {
				Debug.Log("You've Lost!");
				finishOnTime = false;
				end = true;
			}
			
			// Si paso por todos los checkpoints termino a tiempo la carrera (falta lo del tiempo)
			if (checkPointsDone == checkPointsQty) {
				finishOnTime = end = true;
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
