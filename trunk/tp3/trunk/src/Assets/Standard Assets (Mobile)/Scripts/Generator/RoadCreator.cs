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
	public void Generate()
	{
		mountTransform = gameObject.transform;
		mountTransform.position = new Vector3(0, 0.1f, 0);
		
		
		float rotY = 0.0f;
		
		for (int i=0; i<roadSize; i++)
		{
			bool isOk = false;
			
		//	while(isOk){
				int roadChunkIdx = getRandomChunkIndex();
				
				GameObject newGO = UnityEngine.Object.Instantiate(roadChunks[roadChunkIdx]) as GameObject;
				newGO.name = String.Format("part-{0}", i);
				newGO.transform.parent = transform;
				
				newGO.transform.localPosition = mountTransform.TransformPoint(newGO.transform.localPosition);
				newGO.transform.Rotate(new Vector3(0, rotY, 0));
				
				mountTransform = newGO.GetComponent<RoadChunk>().mountPoint;
				
				RaycastHit hit;
				Vector3 rayIni = mountTransform.position + new Vector3(mountTransform.position.x,100, mountTransform.position.z);
				
		 		isOk = Physics.Raycast(rayIni , -Vector3.up, out hit) && !String.Equals(hit.collider.gameObject.name, "Plane");
		//	}	
			
			rotY += mountTransform.localRotation.eulerAngles.y;
		}
		
	}
	
	public void RemoveAll()
	{
		RoadChunk[] rc = gameObject.GetComponentsInChildren<RoadChunk>();
		
		for (int i=0; i<rc.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(rc[i].gameObject);
		}
	}
	
	private int prevIdx = 0;
	private int getRandomChunkIndex()
	{
		// No permitir dos giros consecutivos
		if (prevIdx == 1 || prevIdx == 2)
		{
			prevIdx = 0;
			return 0;
		}
		
		prevIdx = UnityEngine.Random.Range(0, roadChunks.Length);
		return prevIdx;
	}
}
