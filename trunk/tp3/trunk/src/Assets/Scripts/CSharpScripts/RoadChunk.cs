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
		if (collision.gameObject.layer.Equals("Default")) {
			Debug.Log("Destruyendo objeto!");
			DestroyImmediate(collision.gameObject);
		}
		//raceManager.recieveCheckPoint(checkPointIdx);
	}
	
}
