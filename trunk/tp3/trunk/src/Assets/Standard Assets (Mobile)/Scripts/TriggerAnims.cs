using UnityEngine;
using System.Collections;

public class TriggerAnims : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	
	}
	
	void OnGUI()
	{
		if (GUILayout.Button("Jump"))
		{
			gameObject.animation.Play("run");
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
