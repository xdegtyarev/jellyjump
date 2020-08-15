using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game instance;
	private int score;
	private int bestScore;

	bool newRecordScore;

	[SerializeField] LevelControl levelControl;
	[SerializeField] JellyControl jellyControl;
	[SerializeField] UIController uiController;
	[SerializeField] Water waterControl;
	[SerializeField] CameraControl cameraController;

	void Awake(){
		instance = this;
	}

	void Start(){
		uiController.ToggleMainMenuScreen();
	}

	public void Reset(){
		newRecordScore = false;
		score = 0;
		cameraController.Reset();
		levelControl.Reset();
		jellyControl.Reset();
		waterControl.Reset();
		uiController.ToggleGameScreen();
	}

	public void OnEnable(){
		JellyControl.drowned += OnJellyDrowned;
		LevelControl.nextObstacleReached += UpdateScore;
	}

	public void OnDisable(){
		JellyControl.drowned += OnJellyDrowned;
		LevelControl.nextObstacleReached -= UpdateScore;
	}

	public void OnJellyDrowned(){
		uiController.ToggleResultsScreen(newRecordScore, score);
	}

	public void UpdateScore(int newScore){
		score = newScore;
		if(score>bestScore){
			newRecordScore = true;
			bestScore = score;
		}
	}
}
