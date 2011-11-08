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
	public GameObject FinishLine;
	private GameObject [] tempChunks;
		
	/// <summary>
	/// Size of the road in number of chunks
	/// </summary>
	public int roadSize = 10;
	
	
	//private Transform mountTransform;
	private Vector3[] rays;
	private RaycastHit[] hits;
	
	public Transform lastChunk;
	public float lastRotY;
	public int lastIdx;
	
	int counter = 0;
	
	public void Start() {
		
	}
	
	/// <summary>
	/// Generate the level
	/// </summary>
	public void Generate ()
	{
		Transform firstChunk;
		GameObject fline;
		
		// Initialization
		rays = new Vector3[6];
		hits = new RaycastHit[6];
		bool done = false;
		
		while(!done){
			RemoveAll();
			lastRotY=0.0f;
			lastIdx=0;
			putChunks(gameObject.transform, 0.0f, 0, -1);
			firstChunk = transform.Find("part-0");
			fline = UnityEngine.Object.Instantiate (FinishLine) as GameObject;
			fline.transform.position = firstChunk.Find("mountPoint").position;
			
			counter=0;
			
			Debug.Log("Position:  " + transform.position);
			Debug.Log("Size:  ");
			AStarHelper astar = new AStarHelper(firstChunk, this);
			done = astar.run();
					
			if(!done){
			
				Debug.Log("Trying again");
			
			}
		//GameObject inicio = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		//inicio.transform.position= firstChunk.position ;//- new Vector3(0,0,0.25f*firstChunk.GetComponent<BoxCollider>().size.z);
		//Debug.Log(transform.position);
		}
	}
	
	private bool putChunks(Transform mountTransform, float rotY, int iter, int previdx) {
		GameObject currChunk;
		Transform mountPoint;
		bool success, overlap;
		int roadChunkIdx, attemps;
		Dictionary<int, bool> chunksTested;
		
		// End Condition
		if (iter >= roadSize) {
			lastIdx=previdx;
			lastChunk=mountTransform;
			lastRotY=rotY;
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
	
	public bool putChunk(Transform mountTransform, float rotY, GameObject chunk) {
		Transform newChunkTrans, newChunkMount;
		BoxCollider newChunkCollider;	
		
		bool overlap = false;
		chunk.layer = 2;
		
		// positions chunk
		chunk.transform.localPosition = mountTransform.TransformPoint (chunk.transform.localPosition);
		chunk.transform.position = new Vector3(chunk.transform.position.x, 0, chunk.transform.position.z);
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
		rays[2] = newChunkTrans.TransformPoint(new Vector3(6f,8,-0.25f));
		rays[3] = newChunkTrans.TransformPoint(new Vector3(-6f,8,-0.25f));
		rays[4] = newChunkTrans.TransformPoint(new Vector3(-6f,8,-1*newChunkCollider.size.z));
		rays[5] = newChunkTrans.TransformPoint(new Vector3(6f,8,-1*newChunkCollider.size.z));
					
		for (int j = 0 ; j < rays.GetLength(0) ; j++) {
			
			if (!(Physics.Raycast (rays[j], -Vector3.up, out hits[j]) && (String.Equals (hits[j].collider.gameObject.name, "Plane") || String.Equals(hits[j].collider.gameObject.name, chunk.transform.name)))) {
			//	GameObject inicio = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			//	inicio.transform.position= new Vector3(rays[j].x,0,rays[j].z);
			//	inicio.collider.enabled=false;
				
				Debug.Log(hits[j].collider.gameObject.name);
				if(!(hits[j].collider.gameObject.name.Equals("part-0"))){
				   
					overlap = true;
				}
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
	
	public void closePath(ArrayList path, Transform mountTransform, float rotY, int iter) {
		GameObject currChunk;
		Transform mountPoint;
		int roadChunkIdx;
		
		// End Condition
		if (iter >= path.Count) {
			return;
		}
				
		object[] indexes =path.ToArray() ;
		roadChunkIdx = (int)indexes[iter];
		currChunk = UnityEngine.Object.Instantiate (roadChunks[roadChunkIdx]) as GameObject;	
		currChunk.name = String.Format ("part-{0}", roadSize+iter);
		currChunk.transform.parent = transform;
		
		mountPoint = currChunk.GetComponent<RoadChunk>().mountPoint;
		putChunk(mountTransform, rotY, currChunk);
		
		closePath(path, mountPoint, rotY + mountPoint.localRotation.eulerAngles.y, iter+1);
					
		return;
	}
}
