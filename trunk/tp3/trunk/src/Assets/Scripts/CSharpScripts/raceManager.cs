using UnityEngine;
using System.Collections;

public class raceManager : MonoBehaviour {
	public GameObject roadCreator;
	private static int checkPointsDone, checkPointsQty;
	private bool finishOnTime;
	
	// Use this for initialization
	void Start () {
		roadCreator.GetComponent<RoadCreator>().Generate();
		checkPointsDone = 0;
		checkPointsQty = roadCreator.transform.childCount;
		Debug.Log(checkPointsQty);
	}
	
	// Update is called once per frame
	void Update () {
		// Si paso por todos los checkpoints termino a tiempo la carrera (falta lo del tiempo)
		if (checkPointsDone == checkPointsQty) {
			finishOnTime = true;
			Debug.Log("You've Won!");
		}
	}
	
	public static void recieveCheckPoint(int chkidx) {
		if (chkidx - checkPointsDone == 1) {
			checkPointsDone++;
			Debug.Log("checkpoint! " + checkPointsDone);
		}
	}
}
