using UnityEngine;
using System;
using System.Collections;

public class RoadNode : IComparable{
	
	RoadCreator creator;
	public ArrayList pathUntilNow;
	public int costSoFar;
	int chunkIdx;
	Transform destination;
	public Transform mountPoint;
	float rotY;
	bool overlap;
	
	public RoadNode(int idx, Transform prevChunk, float prevRotY, int prevCost, RoadCreator creator, ArrayList pathUntilNow, Transform destiny){
		
		this.creator = creator;
		
		this.chunkIdx=idx;
		
		this.destination = destiny;
		
		this.pathUntilNow = (ArrayList)pathUntilNow.Clone();
		this.pathUntilNow.Add(chunkIdx);
		
		GameObject currentChunk = UnityEngine.Object.Instantiate (creator.roadChunks[chunkIdx]) as GameObject;
		currentChunk.collider.enabled=false;
		currentChunk.name= "Astar";
		currentChunk.tag="starchunk";
		this.overlap = creator.putChunk(prevChunk, prevRotY, currentChunk);
				
		this.mountPoint = currentChunk.GetComponent<RoadChunk>().mountPoint;
		
		this.costSoFar = prevCost + Mathf.Abs((int)(this.mountPoint.transform.localPosition.x)) + Mathf.Abs((int)(this.mountPoint.transform.localPosition.z));
		
		this.rotY = prevRotY + mountPoint.localRotation.eulerAngles.y;
				
	}
	
	public RoadNode[] children(){
		RoadNode[] toReturn;
		if(chunkIdx != 0){
			toReturn = new RoadNode[1];
			toReturn[0] = new RoadNode(0, mountPoint, rotY, costSoFar, creator, pathUntilNow, destination);
			
		} else {
			toReturn = new RoadNode[3];
			toReturn[0] = new RoadNode(0, mountPoint, rotY, costSoFar, creator, pathUntilNow, destination);
			toReturn[1] = new RoadNode(1, mountPoint, rotY, costSoFar, creator, pathUntilNow, destination);
			toReturn[2] = new RoadNode(2, mountPoint, rotY, costSoFar, creator, pathUntilNow, destination);
		}
		
		return toReturn;
	}
	
	public bool isValid(){
		return !overlap;
	}
	
	
	public int heuristicCost(Transform destination){
		return costSoFar + (int)Math.Abs(mountPoint.transform.position.x - destination.position.x) + (int)Math.Abs(mountPoint.transform.position.z - destination.position.z);
	}
		
	public int CompareTo (object other){
		RoadNode otherRoad = other as RoadNode;
		if(otherRoad != null){
			if (this.heuristicCost(destination) > otherRoad.heuristicCost(destination))
				return 1;
			if (this.heuristicCost(destination) < otherRoad.heuristicCost(destination))
				return -1;
			else{
				if(this.mountPoint.name.Equals(otherRoad.mountPoint.name))
					return 0;
				else
					return -1; // quiero que me los diferencie sí o sí, por eso -1 en lugar de 0.
			}
		} else {
			throw new ArgumentException("The object is no RoadNode");
		}
	}
}
