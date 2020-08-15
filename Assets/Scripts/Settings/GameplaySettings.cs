using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySettings : ScriptableObject
{
	[Header("Level")]
	[SerializeField] float initialVerticalScrollSpeed;
	[SerializeField] float verticalScrollSpeedAcceleration;

	[Header("Character")]
	[SerializeField] float jumpAcc;
	[SerializeField] float gravity;
}
