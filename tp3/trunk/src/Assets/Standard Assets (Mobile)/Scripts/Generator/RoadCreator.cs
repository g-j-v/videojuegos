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
			
			newGO.transform.localPosition = mountTransform.TransformPoint (newGO.transform.localPosition);
			newGO.transform.Rotate (new Vector3 (0, rotY, 0));
			
			Vector3 initialPosition = newGO.GetComponent<BoxCollider>().transform.position;
			Vector3 rayIni2 = newGO.GetComponent<RoadChunk>().mountPoint.position;
			Vector3 rayIni1 = initialPosition + (rayIni2-initialPosition)/2;
			
			/*GameObject collision1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			collision1.transform.position = rayIni1;
			collision1.name = "coll1-"+ i;
			
			GameObject collision2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			collision2.transform.position = rayIni2;
			collision2.name = "coll2-"+ i;*/
			
			
			RaycastHit hit1;
			RaycastHit hit2;
			
			bool condition1 = Physics.Raycast (rayIni1, -Vector3.up, out hit1) && (String.Equals (hit1.collider.gameObject.name, "Plane"));// || Equals(hit1.collider.gameObject, newGO.transform));
			bool condition2 = Physics.Raycast (rayIni2, -Vector3.up, out hit2) && (String.Equals (hit2.collider.gameObject.name, "Plane"));// || String.Equals (hit2.collider.gameObject.name, newGO.transform.name));
			
			//Debug.Log(condition1 + "  " + condition2);
			Debug.Log(hit1.collider.gameObject.name + "   " + hit2.collider.gameObject.name + "    " + newGO.transform.name);
			//Debug.Log(rayIni1 + "   " + rayIni2 );
			
			if (condition1 && condition2) {
				mountTransform = newGO.GetComponent<RoadChunk>().mountPoint;
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
