using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[SerializeField] float speed;
	Transform _cachedTransform;
	float _targetYPosition = 0f;
	void Awake(){
		_cachedTransform = transform;
	}

	public void Reset(){
		_cachedTransform.position = Vector3.zero;
		_targetYPosition = 0f;
	}

	void OnEnable(){
		JellyControl.landedOnPlatform+=UpdateTargetPos;
	}

	void OnDisable(){
		JellyControl.landedOnPlatform-=UpdateTargetPos;
	}

	void UpdateTargetPos(Obstacle obstacle){
		_targetYPosition = obstacle.transform.position.y;
	}

	void Update(){
		var diff = _targetYPosition-_cachedTransform.position.y;
		if(Mathf.Abs(diff) > speed*Time.deltaTime){
			_cachedTransform.position += Vector3.up*Mathf.Sign(diff)*speed*Time.deltaTime;
		}
	}
}
