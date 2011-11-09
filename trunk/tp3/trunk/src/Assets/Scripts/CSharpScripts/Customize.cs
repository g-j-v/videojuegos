using System;
using UnityEngine;
using System.Collections;


// Make the script also execute in edit mode
[ExecuteInEditMode]
public class Customize : MonoBehaviour
{
	public GUISkin gSkin;
	public Texture2D backdrop;
	private bool isLoading;
	private GUIStyle backgroundStyle;
	private float initialChunks, customTime;

	// Use this for initialization
	void Start ()
	{
		backgroundStyle = new GUIStyle ();
		initialChunks = 15f;
		customTime = 3 * initialChunks;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnGUI ()
	{
		float titleWidth, loadingWidth, titleYPos, titleHeight, buttonHeight, space, sliderHeight, labelHeight;
		float buttonWidth = 140;
		
		titleWidth = loadingWidth = 400;
		buttonHeight = titleYPos = 70;
		titleHeight = 100;
		sliderHeight = space = 10;
		labelHeight = 20;
		float yPos = 0;
		
		
		if (gSkin) {
			GUI.skin = gSkin;
			backgroundStyle = new GUIStyle ();
			backgroundStyle.normal.background = backdrop;
			GUI.Label (new Rect (0f, 0f, Screen.width, Screen.height), "", backgroundStyle);
			yPos = titleYPos;
			GUI.Label (new Rect (((Screen.width - titleWidth) / 2), yPos, titleWidth, titleHeight), "Customize", "mainMenuTitle");
			yPos+=titleHeight + space;
			
			
			if (!isLoading) {
				
				GUI.Label (new Rect (((Screen.width - buttonWidth) / 2), yPos , buttonWidth, labelHeight), "Initial Road Size");
				yPos+= labelHeight + space;
				initialChunks = GUI.HorizontalSlider (new Rect (((Screen.width - buttonWidth) / 2), yPos, buttonWidth, sliderHeight), initialChunks, 10f, 75f);
				yPos+= sliderHeight + space;
				GUI.Label (new Rect (((Screen.width - buttonWidth) / 2), yPos , buttonWidth, labelHeight), ((int)initialChunks).ToString());
				yPos+= labelHeight + space;
				
				yPos+=2*space;
				GUI.Label (new Rect (((Screen.width - buttonWidth) / 2), yPos, buttonWidth, labelHeight), "Race Time");
				yPos+= labelHeight + space;
				customTime = GUI.HorizontalSlider (new Rect (((Screen.width - buttonWidth) / 2), yPos, buttonWidth, sliderHeight), Math.Max (customTime, 2 * initialChunks), initialChunks * 2, initialChunks * 4);
				yPos+= sliderHeight + space;
				GUI.Label (new Rect (((Screen.width - buttonWidth) / 2), yPos , buttonWidth, labelHeight), customTime.ToString());
				yPos+= labelHeight + space;
				
				
				yPos+=5*space;
				if (GUI.Button (new Rect (((Screen.width - buttonWidth) / 2), yPos, buttonWidth+20, buttonHeight), "Start Race")) {
					isLoading = true;
					SceneParameters.initialSize=(int)initialChunks;
					SceneParameters.time=customTime;
					Application.LoadLevel ("Scene1");
				}
				
			} else {
				GUI.Label (new Rect (((Screen.width - loadingWidth) / 2), (Screen.height / 2), loadingWidth, 70), "Loading...", "mainMenuTitle");
			}
		} else {
			Debug.Log ("StartMenuGUI: GUI Skin object missing!");
		}
	}
}
