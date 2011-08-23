using UnityEngine;
using System.Collections;


// Make the script also execute in edit mode
[ExecuteInEditMode]
public class StartMenuGUI: MonoBehaviour {
	public GUISkin gSkin;
	public Texture2D backdrop;
	private bool isLoading; 
	private GUIStyle backgroundStyle;
	
	// Use this for initialization
	void Start () {
		backgroundStyle = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		if (gSkin)
		{
			GUI.skin = gSkin;
			backgroundStyle = new GUIStyle();
			backgroundStyle.normal.background = backdrop;
			GUI.Label (new Rect((Screen.width - (Screen.height * 2f)) * 0.75f, 0f, Screen.height * 2f, Screen.height), "", backgroundStyle);
			GUI.Label (new Rect( (Screen.width/2), 50, 400, 100), "Space Invaders", "mainMenuTitle");
			
			if (GUI.Button( new Rect( (Screen.width/2), Screen.height/2, 140, 70), "Play"))
			{
				isLoading = true;
				Application.LoadLevel("invaders");
			}
			
			 if (isLoading) 
			{
				GUI.Label ( new Rect( (Screen.width/2), (Screen.height / 2), 400, 70), "Loading...", "mainMenuTitle"); 
			}
		}
		else
		{
			Debug.Log("StartMenuGUI: GUI Skin object missing!");
		}
	}
}