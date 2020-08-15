using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuScreen : UIScreen {
  	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Game.instance.Reset();
		}
	}
}
