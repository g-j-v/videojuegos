using System;
using System.Collections;
using System.Collections.Generic;
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
	private Vector3[] rays;
	private RaycastHit[] hits;
	
	public void Start() {
		
	}
	
	/// <summary>
	/// Generate the level
	/// </summary>
	public void Generate ()
	{
		
		// Initialization
		rays = new Vector3[6];
		hits = new RaycastHit[6];
		
		putChunks(gameObject.transform, 0.0f, 0, -1);
	}
	
	private bool putChunks(Transform mountTransform, float rotY, int iter, int previdx) {
		GameObject currChunk;
		Transform mountPoint;
		bool success, overlap;
		int roadChunkIdx, attemps;
		Dictionary<int, bool> chunksTested;
		
		// End Condition
		if (iter >= roadSize) {
			return true;
		}
			
		success = overlap = false;
		attemps = 0;
		
		chunksTested = new Dictionary<int, bool>();
		
		for (int i = 0 ; i < roadChunks.Length ; i++) {
			chunksTested.Add(i,false);
		}	
		
		while(attemps < roadChunks.Length && !success){
			roadChunkIdx = getRandomChunkIndex (chunksTested, previdx);
			currChunk = UnityEngine.Object.Instantiate (roadChunks[roadChunkIdx]) as GameObject;	
			currChunk.name = String.Format ("part-{0}", iter);
			currChunk.transform.parent = transform;
			mountPoint = currChunk.GetComponent<RoadChunk>().mountPoint;
			overlap = putChunk(mountTransform, rotY, currChunk);
			attemps++;
				
			if (overlap) {
				DestroyImmediate(currChunk);
			} else {
				success = putChunks(mountPoint, rotY + mountPoint.localRotation.eulerAngles.y, iter+1, 
									roadChunkIdx);
					
				if (!success) {
					DestroyImmediate(currChunk);	
				}
			}
		}
		
		return success;
	}
	
	public void RemoveAll ()
	{
		RoadChunk[] rc = gameObject.GetComponentsInChildren<RoadChunk> ();
		
		for (int i = 0; i < rc.Length; i++) {
			UnityEngine.Object.DestroyImmediate (rc[i].gameObject);
		}
	}
	
	private bool putChunk(Transform mountTransform, float rotY, GameObject chunk) {
		Transform newChunkTrans, newChunkMount;
		BoxCollider newChunkCollider;	
		
		bool overlap = false;
		chunk.layer = 2;
		
		// positions chunk
		chunk.transform.localPosition = mountTransform.TransformPoint (chunk.transform.localPosition);
		chunk.transform.Rotate (new Vector3 (0, rotY, 0));
		
		newChunkTrans = chunk.GetComponent<BoxCollider>().transform;
		newChunkCollider = chunk.GetComponent<BoxCollider>();
		newChunkMount = chunk.GetComponent<RoadChunk>().mountPoint;
		
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
					
		for (int j = 0 ; j < rays.GetLength(0) ; j++) {
			if (!(Physics.Raycast (rays[j], -Vector3.up, out hits[j]) && (String.Equals (hits[j].collider.gameObject.name, "Plane") || String.Equals(hits[j].collider.gameObject.name, chunk.transform.name)))) {
				overlap = true;
			}
		}
		
		chunk.layer = 0;
		return overlap;
	}
	
	private int getRandomChunkIndex (Dictionary<int, bool> chunksTested, int previdx)
	{
		int idx = 0;
		bool prevSelected = true;
		
		// No permitir dos giros consecutivos
		if (previdx == 1 || previdx == 2) {
			chunksTested[0] = true;
			return 0;
		}
		
		while(prevSelected) {
			idx = UnityEngine.Random.Range (0, roadChunks.Length);
			chunksTested.TryGetValue(idx, out prevSelected);
			
			if (!prevSelected) {
				chunksTested[idx] = true;
			}
		}
			    
		return idx;
	}
}