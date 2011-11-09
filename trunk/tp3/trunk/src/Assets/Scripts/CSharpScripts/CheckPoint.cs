using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	public int checkidx;
	public static GameObject[] checkPoints;
	public static int currCheck;
	public Material nextCheck, farCheck;
	
	// Use this for initialization
	void Start () {
		currCheck = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 8) {
			if (checkidx - 1 == currCheck) {
				checkPoints[checkidx].transform.Find("finishSign").renderer.material = farCheck;
				currCheck++;
				raceManager.receiveCheckPoint();
				if (checkidx + 1 < checkPoints.Length) {
					checkPoints[checkidx+1].transform.Find("finishSign").renderer.material = nextCheck;
				}
			}
			
			// segunda pasada por el checkpoint inicial
			if (currCheck == checkPoints.Length && checkidx == 0) {
				currCheck++;
				raceManager.receiveCheckPoint();
			}
			
		}
    }
}
