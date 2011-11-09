using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	public int checkidx;
	public static Transform[] checkPoints;
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
        if (other.gameObject.layer.Equals("car")) {
			checkPoints[checkidx].Find("finishSign").renderer.material = farCheck;
			currCheck++;
			if (checkidx + 1 <= checkPoints.Length) {
				checkPoints[checkidx+1].Find("finishSign").renderer.material = nextCheck;
			}
		}
    }
}
