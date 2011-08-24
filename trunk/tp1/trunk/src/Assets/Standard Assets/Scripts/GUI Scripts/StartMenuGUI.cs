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
		float titleWidth, loadingWidth;
		float buttonWidth = 140;
		
		titleWidth = loadingWidth = 400;
		
		if (gSkin)
		{
			GUI.skin = gSkin;
			backgroundStyle = new GUIStyle();
			backgroundStyle.normal.background = backdrop;
			GUI.Label (new Rect(0f, 0f, Screen.width, Screen.height), "", backgroundStyle);
			GUI.Label (new Rect( ((Screen.width-titleWidth)/2), 50, titleWidth, 100), "Space Invaders", "mainMenuTitle");
			
			if (!isLoading)
			{
				if (GUI.Button( new Rect( ((Screen.width-buttonWidth)/2), Screen.height/2, buttonWidth, 70), "Play"))
				{
					isLoading = true;
					Application.LoadLevel("invaders");
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