using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsControl : MonoBehaviour {
	const float wallHeight = 15f;
	[SerializeField] Transform[] walls;
	[SerializeField] Transform jelly;

	public void Reset(){
		for (int i = 0; i < walls.Length; i++) {
			walls[i].localPosition = Vector3.up * wallHeight * i;
		}
	}

	void Update() {
		for (int i = 0; i < walls.Length; i++) {
			if ((jelly.position.y - wallHeight*2)  > walls[i].position.y) {
				walls[i].position += Vector3.up * wallHeight * walls.Length;
			}
		}
	}
}
