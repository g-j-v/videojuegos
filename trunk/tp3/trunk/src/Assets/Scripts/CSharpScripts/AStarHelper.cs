using UnityEngine;
using System.Collections;

public class AStarHelper
{

	SortedList open = new SortedList();
	
	RoadCreator creator;
	static Transform destination;
	private int expanded = 0;

	
		
	
	public AStarHelper(Transform destiny, RoadCreator creator){
		
		this.creator = creator;
		destination = destiny;
		
		ArrayList original = new ArrayList();
		
		if(creator.lastIdx == 0){
			RoadNode rn0 = new RoadNode(0, creator.lastChunk, creator.lastRotY, 0, creator, original, destination);
			rn0.mountPoint.name = "Astar-0";
			open.Add(rn0, rn0);
			RoadNode rn1 = new RoadNode(1, creator.lastChunk, creator.lastRotY, 0, creator, original, destination);
			rn1.mountPoint.name = "Astar-1";
			open.Add(rn1, rn1);
			RoadNode rn2 = new RoadNode(2, creator.lastChunk, creator.lastRotY, 0, creator, original, destination);
			rn2.mountPoint.name = "Astar-2";
			open.Add(rn2, rn2);
			expanded=3;
			
		} else {
			RoadNode rn0 = new RoadNode(0, creator.lastChunk, creator.lastRotY, 0, creator, original, destination);
			rn0.mountPoint.name = "Astar-0";
			open.Add(rn0, rn0);
			expanded=1;
		}
	}
	
	public bool run(){
		for(int j=0; j<3*creator.roadSize && open.Count != 0;j++){
			
			RoadNode node = (RoadNode) open.GetByIndex(0);
						
			if(isEnd(node)){
				creator.closePath(node.pathUntilNow, creator.lastChunk, creator.lastRotY , 0);
				
				Debug.Log("Hay solución");
				removeFinderChunks();
				return true;
			} else {
				open.RemoveAt(0);
				if(node.isValid()){
					RoadNode[] children = node.children();
					for(int i = 0; i<children.Length;i++){
						children[i].mountPoint.name = "Astar-"+expanded;
						open.Add(children[i], children[i]);
						expanded++;
					}
				}
			}
			
		}
		
		if(open.Count == 0){
			removeFinderChunks();
			Debug.Log("No hay solución");
			return false;
		}
		removeFinderChunks();
		Debug.Log("No hay solución 2");
		return false;
	}
	 
	bool isEnd(RoadNode node){
		return Vector3.Distance(node.mountPoint.position,destination.position) < 6 && Quaternion.Angle(node.mountPoint.rotation,destination.rotation) < 1;
	}
	
	
	void removeFinderChunks(){
		GameObject[] chunks = GameObject.FindGameObjectsWithTag("starchunk");
		for(int i= 0; i<chunks.Length;i++){
			GameObject.DestroyImmediate(chunks[i]);
		}
	}
		
}
