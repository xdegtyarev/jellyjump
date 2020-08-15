using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] UIResultsScreen resultsScreen;
	[SerializeField] UIMainMenuScreen mainMenuScreen;
	[SerializeField] UIGameScreen gameScreen;

	private GameObject currentActiveScreen;


	public void CloseActiveScreen(){
		if(currentActiveScreen!=null){
			currentActiveScreen.SetActive(false);
		}
	}

	public void OpenScreen(UIScreen screen){
		CloseActiveScreen();
		currentActiveScreen = screen.gameObject;
		currentActiveScreen.SetActive(true);
	}

	public void ToggleMainMenuScreen(){
		OpenScreen(mainMenuScreen);
	}

	public void ToggleResultsScreen(bool bestScore, int score){
		OpenScreen(resultsScreen);
		resultsScreen.Setup(bestScore, score);
	}

	public void ToggleGameScreen(){
		OpenScreen(gameScreen);
	}
}
