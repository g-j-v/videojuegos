using UnityEngine;
using System.Collections;

public class RoadChunk : MonoBehaviour
{
	public Transform mountPoint;
	private int checkPointIdx;
	
	public int pCheckPointIdx {
		get {return checkPointIdx;}
		set {checkPointIdx = value;}
	}
	
	public void OnCollisionEnter(Collision collision) {
		raceManager.recieveCheckPoint(checkPointIdx);
	}
	
}
