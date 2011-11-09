using UnityEngine;
using System.Collections;

public class raceManager : MonoBehaviour {
	public GUISkin gSkin;
	public GameObject roadCreator, firstCamera, thirdCamera;
	public GameObject CheckPointLine;
	public Texture2D redLight, yellowLight, greenLight;
	public GameObject carGO;
	private static int checkPointsDone, checkPointsQty;
	private bool finishOnTime, start, end;
	private float timeLimit = 60.0f;
	private float invocationTime;
	float resetTime;
	
	// Use this for initialization
	void Start () {
		
		invocationTime = Time.time;
		
		roadCreator.GetComponent<RoadCreator>().Generate();
		
		resetTime=0f;
		
		checkPointsDone = 0;
		checkPointsQty = (int)(roadCreator.transform.childCount/10);
		
		CheckPoint.checkPoints = new GameObject[checkPointsQty];
		Debug.Log("Cantidad checks: " + checkPointsQty);
		for(int i =0; i<checkPointsQty; i++){
			putCheckPoint(i);
		}
		
		CheckPoint.currCheck = 0;
		
		start = end = false;
		timeLimit = SceneParameters.time;
		Debug.Log(checkPointsQty);
	}
	
	void putCheckPoint(int index){
			Transform currChunk = roadCreator.GetComponent<RoadCreator>().transform.GetChild(index*10);
			Transform mountpoint = currChunk.GetComponent<RoadChunk>().mountPoint;
			
			GameObject fline = UnityEngine.Object.Instantiate(CheckPointLine) as GameObject;
			Vector3 planePos=mountpoint.position;
			fline.transform.position = new Vector3(planePos.x, 6f, planePos.z);
			fline.transform.rotation=mountpoint.transform.rotation;
		
			fline.GetComponent<CheckPoint>().checkidx = index;
			CheckPoint.checkPoints[index]=fline;
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
		if (Time.time-invocationTime < 2.0f) {
        	GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), redLight);
		} else if (Time.time-invocationTime < 4.0f) {
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.10f, Screen.height * 0.20f), yellowLight);
			
		} else if (Time.time-invocationTime < 6.0f) {
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
			if (Time.time - invocationTime > 1f) {
				carGO.GetComponent<Rigidbody>().constraints = carGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
		}
	}
	
	public static void receiveCheckPoint() {
		checkPointsDone++;
		Debug.Log(checkPointsDone);
	}
}
