using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameScreen : UIScreen
{
	[SerializeField] TMP_Text text;
    void OnEnable(){
    	LevelControl.nextObstacleReached += UpdateLabel;
    }

    void OnDisable(){
    	LevelControl.nextObstacleReached += UpdateLabel;
    }

    void UpdateLabel(int val){
    	text.text = val.ToString();
    }
}
