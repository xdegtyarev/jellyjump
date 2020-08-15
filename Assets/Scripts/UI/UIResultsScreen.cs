using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResultsScreen : UIScreen
{
	[SerializeField] TMP_Text scoreLabel;
	[SerializeField] TMP_Text bestScoreLabel;
  	public void Setup(bool bestScore, int score){
  		bestScoreLabel.gameObject.SetActive(bestScore);
  		scoreLabel.text = score.ToString();
  	}

  	public void OnClick(){
  		Game.instance.Reset();
  	}
}