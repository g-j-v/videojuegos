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
		float titleWidth, loadingWidth, titleYPos, titleHeight, buttonHeight, space;
		float buttonWidth = 140;
		
		titleWidth = loadingWidth = 400;
		buttonHeight=titleYPos=70;
		titleHeight=100;
		space=10;
		
		if (gSkin)
		{
			GUI.skin = gSkin;
			backgroundStyle = new GUIStyle();
			backgroundStyle.normal.background = backdrop;
			GUI.Label (new Rect(0f, 0f, Screen.width, Screen.height), "", backgroundStyle);
			GUI.Label (new Rect( ((Screen.width-titleWidth)/2), titleYPos, titleWidth, titleHeight), "RACE CAR", "mainMenuTitle");
			
			//GUI.Label (new Rect(0, Screen.height/2, 250, 100), " Player 1: A D \n Player 2: <- -> \n\n Pause: P, esc", "textfield");

			if (!isLoading)
			{
				if (GUI.Button( new Rect( ((Screen.width-buttonWidth)/2), titleYPos+titleHeight+space, buttonWidth, buttonHeight), "Easy"))
				{
					isLoading = true;
					SceneParameters.initialSize = 10;
					SceneParameters.time=40.0f;
					Application.LoadLevel("Scene1");
				}
				
				if (GUI.Button( new Rect( ((Screen.width-buttonWidth)/2), titleYPos+titleHeight+buttonHeight+2*space, buttonWidth, buttonHeight), "Medium"))
				{
					isLoading = true;
					SceneParameters.initialSize = 30;
					SceneParameters.time=70.0f;
					Application.LoadLevel("Scene1");
				}
				
				if (GUI.Button( new Rect( ((Screen.width-buttonWidth)/2),  titleYPos+titleHeight+2*buttonHeight+3*space, buttonWidth, buttonHeight), "Hard"))
				{
					isLoading = true;
					SceneParameters.initialSize=50;
					SceneParameters.time=100.0f;
					Application.LoadLevel("Scene1");
				}
				
				if (GUI.Button( new Rect( ((Screen.width-buttonWidth)/2), titleYPos+titleHeight+3*buttonHeight+5*space, buttonWidth, buttonHeight), "Custom"))
				{
					Application.LoadLevel("Customize");
				}
			
			}	
			else 
			{
				GUI.Label ( new Rect( ((Screen.width-loadingWidth)/2), (Screen.height / 2), loadingWidth, 70), "Loading...", "mainMenuTitle"); 
			}
		}
		else
		{
			Debug.Log("StartMenuGUI: GUI Skin object missing!");
		}
	}
}