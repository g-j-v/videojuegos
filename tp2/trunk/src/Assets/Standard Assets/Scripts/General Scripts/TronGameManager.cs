using UnityEngine;
using System.Collections;

public class TronGameManager : MonoBehaviour {
	public GUISkin gSkin;
	public static int playerWin;
	public static bool finishGame;
	private float resetTime;
	
	// Use this for initialization
	void Start () {
		finishGame = false;
		playerWin = 0;
	}
	
	void OnGUI() {
		int buttonwidth = 200;
		int labelwidth = 100;
		GUI.skin = gSkin;
		
		GUI.Label(new Rect(10, 10, labelwidth, 25), "Player 1", "textfield");
		GUI.Label(new Rect(Screen.width /2 + 10, 10, labelwidth, 25), "Player 2", "textfield");
		
		
		if (Time.timeScale == 0) {
			
			if(GUI.Button(new Rect((Screen.width - buttonwidth)/2, 70, buttonwidth, 50), "Resume"))
			{
				ResumeGame();
			}
			
		}
		
		if(finishGame) {
			if (resetTime == 0) {
				resetTime = Time.time;
			}
			string winner;
			
			if(playerWin == 0){
				winner = "Both lost.";
			} else {
				winner = "Player "+ playerWin + " won.";
			}
			GUI.Label(new Rect((Screen.width - buttonwidth)/2, 70, buttonwidth, 50), winner, "mainMenuTitle");
			
			if (Time.time > resetTime + 5) {
				Application.LoadLevel("GUIScene");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Time.timeScale);
		if (!finishGame) {
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				PauseGame();
			}
		}
	}
	
	void PauseGame() {
		Time.timeScale = 0;
	}
	
	void ResumeGame() {
		Time.timeScale = 1.0f;
	}
	
	public static void playerOneWon() {
		playerWin = 1;
		finishGame = true;
	}
	
	public static void playerTwoWon() {
		playerWin = 2;
		finishGame = true;
	}
	
	public static void evenGame() {
		finishGame = true;
	}
}
