using System;
using System.Collections;

using UnityEngine;

[ExecuteInEditMode]
public class RoadCreator : MonoBehaviour
{
	/// <summary>
	/// Chunks of the road
	/// </summary>
	public GameObject[] roadChunks;

	/// <summary>
	/// Size of the road in number of chunks
	/// </summary>
	public int roadSize = 10;

	private Transform mountTransform;

	/// <summary>
	/// Generate the level
	/// </summary>
	public void Generate ()
	{
		mountTransform = gameObject.transform;
		mountTransform.position = new Vector3 (0, 0.1f, 0);
		
		
		float rotY = 0.0f;
		
		for (int i = 0; i < roadSize; i++) {

			int roadChunkIdx = getRandomChunkIndex ();
			
			GameObject newGO = UnityEngine.Object.Instantiate (roadChunks[roadChunkIdx]) as GameObject;
			newGO.name = String.Format ("part-{0}", i);
			newGO.transform.parent = transform;
			newGO.transform.Rotate (new Vector3 (0, rotY, 0));
			
			Transform tmpMountTransform = newGO.GetComponent<RoadChunk> ().mountPoint;
			Transform tmpCenterTransform = newGO.GetComponent<RoadChunk>().transform;
			Vector3 rayIni1 = mountTransform.position + new Vector3 (tmpMountTransform.position.x, 100, tmpMountTransform.position.z);
			Vector3 rayIni2 = mountTransform.position + new Vector3 (tmpCenterTransform.position.x, 100, tmpCenterTransform.position.z);
			RaycastHit hit1;
			RaycastHit hit2;
			
			bool condition1 = Physics.Raycast (rayIni1, -Vector3.up, out hit1) && String.Equals (hit1.collider.gameObject.name, "Plane");
			bool condition2 = Physics.Raycast (rayIni2, -Vector3.up, out hit2) && (String.Equals (hit2.collider.gameObject.name, "Plane") || String.Equals (hit2.collider.gameObject.name, newGO.name));
			
			Debug.Log(condition1 + "  " + condition2);
			Debug.Log(hit1.collider.gameObject.name + "   " + hit2.collider.gameObject.name);
			Debug.Log(rayIni1 + "   " + rayIni2 );
			
			if (condition1 && condition2) {
				
				newGO.transform.localPosition = mountTransform.TransformPoint (newGO.transform.localPosition);
				
				mountTransform = tmpMountTransform;
				rotY += mountTransform.localRotation.eulerAngles.y;
			} else {
				DestroyImmediate(newGO);
			}
			
		}
		
	}

	public void RemoveAll ()
	{
		RoadChunk[] rc = gameObject.GetComponentsInChildren<RoadChunk> ();
		
		for (int i = 0; i < rc.Length; i++) {
			UnityEngine.Object.DestroyImmediate (rc[i].gameObject);
		}
	}

	private int prevIdx = 0;
	private int getRandomChunkIndex ()
	{
		// No permitir dos giros consecutivos
		if (prevIdx == 1 || prevIdx == 2) {
			prevIdx = 0;
			return 0;
		}
		
		prevIdx = UnityEngine.Random.Range (0, roadChunks.Length);
		return prevIdx;
	}
}
