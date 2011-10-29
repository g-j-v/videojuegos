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
	
	private int prevIdx = 0;
	private Transform mountTransform;
	private Vector3[] rays;
	private RaycastHit[] hits;
	
	public void Start() {
		
	}
	
	/// <summary>
	/// Generate the level
	/// </summary>
	public void Generate ()
	{
		mountTransform = gameObject.transform;
		Transform newChunkTrans, newChunkMount;
		BoxCollider newChunkCollider;
		
		// Initialization
		rays = new Vector3[6];
		hits = new RaycastHit[6];
		
		float rotY = 0.0f;
		bool overlap = false;
		
		for (int i = 0; i < roadSize; i++) {

			int roadChunkIdx = getRandomChunkIndex ();
			
			GameObject newGO = UnityEngine.Object.Instantiate (roadChunks[roadChunkIdx]) as GameObject;
			
			// put chunk to IgnoreRayCast layer temporarily for overlap checking
			newGO.layer = 2; 
			
			newGO.name = String.Format ("part-{0}", i);
			newGO.transform.parent = transform;
			
			// positions chunk
			newGO.transform.localPosition = mountTransform.TransformPoint (newGO.transform.localPosition);
			newGO.transform.Rotate (new Vector3 (0, rotY, 0));
			
			newChunkTrans = newGO.GetComponent<BoxCollider>().transform;
			newChunkCollider = newGO.GetComponent<BoxCollider>();
			newChunkMount = newGO.GetComponent<RoadChunk>().mountPoint;
			
			Vector3 initialPosition = new Vector3(newChunkTrans.position.x, 3, newChunkTrans.position.z);
			
			// chunk mountpoint
			rays[0] = new Vector3(newChunkMount.position.x, 3, newChunkMount.position.z);
			
			// chunk center
			rays[1] = initialPosition + (rays[0]-initialPosition)/2;
			
			// Corners of chunk box collider
			rays[2] = newChunkTrans.TransformPoint(new Vector3(1.5f,2,-0.25f));
			rays[3] = newChunkTrans.TransformPoint(new Vector3(-1.5f,2,-0.25f));
			rays[4] = newChunkTrans.TransformPoint(new Vector3(-1.5f,2,-1*newChunkCollider.size.z));
			rays[5] = newChunkTrans.TransformPoint(new Vector3(1.5f,2,-1*newChunkCollider.size.z));
			
			/*
			GameObject collision1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			collision1.transform.position = rayIni1;
			collision1.name = "coll1-"+ i;
			*/
			/*
			GameObject collision2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			collision2.transform.position = corner2;
			collision2.name = "coll2-"+ i;
			*/
			/*
			GameObject collision3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			collision3.transform.position = corner3;
			collision3.name = "coll3-"+ i;
			*/
			/*
			GameObject collision4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			collision4.transform.position = corner4;
			collision4.name = "coll4-"+ i;
			*/
			
			
			for (int j = 0 ; j < rays.GetLength(0) ; j++) {
				if (!(Physics.Raycast (rays[j], -Vector3.up, out hits[j]) && (String.Equals (hits[j].collider.gameObject.name, "Plane") || String.Equals(hits[j].collider.gameObject.name, newGO.transform.name)))) {
					overlap = true;
				}
			}
			
			if (!overlap) {
				prevIdx = roadChunkIdx;
				mountTransform = newGO.GetComponent<RoadChunk>().mountPoint;
				rotY += mountTransform.localRotation.eulerAngles.y;
				newGO.layer = 0; // restore default layer to chunk
			} else {
				DestroyImmediate(newGO);
			}
			
			// reset overlap test
			overlap = false;
		}
	}

	public void RemoveAll ()
	{
		RoadChunk[] rc = gameObject.GetComponentsInChildren<RoadChunk> ();
		
		for (int i = 0; i < rc.Length; i++) {
			UnityEngine.Object.DestroyImmediate (rc[i].gameObject);
		}
	}

	private int getRandomChunkIndex ()
	{
		// No permitir dos giros consecutivos
		if (prevIdx == 1 || prevIdx == 2) {
			return 0;
		}
			
		return UnityEngine.Random.Range (0, roadChunks.Length);
	}
}
