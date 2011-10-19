using System;
using System.Collections;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator))]
public class RoadCreatorInspector : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if (GUILayout.Button("Generate"))
		{
			GameObject.Find("RoadCreator").GetComponent<RoadCreator>().Generate();
		}
		
		if (GUILayout.Button("Remove All"))
		{
			GameObject.Find("RoadCreator").GetComponent<RoadCreator>().RemoveAll();
		}
	}
}
