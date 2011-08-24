using UnityEngine;
using System.Collections;


// Make the script also execute in edit mode
[ExecuteInEditMode]
public class GameHUD: MonoBehaviour {
	public GUISkin gSkin;
	private float restartTime;
	private bool finishGame;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		float titleWidth;
		float paddingRight = 40;
		string finishLabel = "";
		titleWidth = 400;
		
		
		
		if (gSkin)
		{
			GUI.skin = gSkin;
			GUI.Label (new Rect( ((Screen.width-titleWidth))-paddingRight, 20, titleWidth, 100), "Score: " + InvadersGameData.Score.ToString() , "scoreTitle");
			
			if (InvadersGameData.remainingInvaders == 0)
			{
				finishLabel = "You Won!";
				finishGame = true;
				
				if (restartTime == 0)
				{
					restartTime = Time.time;
				}
			}
			
			if (InvadersGameData.gameLost == true)
			{
				finishLabel = "Game Over!";
				finishGame = true;
				if (restartTime == 0)
				{
					restartTime = Time.time;
				}
			}
			
			Debug.Log(restartTime);
			if (finishGame)
			{
				GUI.Label (new Rect( ((Screen.width-titleWidth)/2), 50, titleWidth, 100), finishLabel, "mainMenuTitle");
				if (Time.time > restartTime + 4)
				{
					Application.LoadLevel("invadersGUI");
				}
			}
		}
		else
		{
			Debug.Log("StartMenuGUI: GUI Skin object missing!");
		}
	}
}