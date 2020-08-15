using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class JellyControl : MonoBehaviour {
	public static event Action<Obstacle> landedOnPlatform;
	public static event Action drowned;

	[SerializeField] GameObject view;
	[SerializeField] float jumpScale = 1f;

	private bool _applyJumpForce;
	private bool _isJumping;
	private bool _isDrowned;

	private Rigidbody _cachedRigidbody;
	private Transform _cachedTransform;

	void Awake() {
		_cachedRigidbody = GetComponent<Rigidbody>();
		_cachedTransform = GetComponent<Transform>();
	}

	public void Reset(){
		_cachedTransform.position = Vector3.zero;
		_isDrowned = false;
		_isJumping = false;
		_applyJumpForce = false;
	}

	void OnJumpAction() {
		if (!_isJumping) {
			_applyJumpForce = true;
			_isJumping = true;
		}
	}

	void OnCollisionStay(Collision collision) {
		var obstacle = collision.gameObject.GetComponent<Obstacle>();
		if (obstacle != null) {
			if (obstacle.transform.position.y < _cachedTransform.position.y) {
				//Means we landed on top of platform
				landedOnPlatform(obstacle);
				_isJumping = false;
			}
		}
	}

	//water is set with trigger colliders - so no check fo origin of collider needed
	void OnTriggerStay(Collider collider) {
		if(!_isDrowned){
			if (collider.transform.position.y > _cachedTransform.position.y) {
				_isDrowned = true;
				drowned();
			}
		}
	}

	void FixedUpdate() {
		if (_applyJumpForce) {
			_cachedRigidbody.AddForce(Vector3.up * jumpScale);
			_applyJumpForce = false;
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			OnJumpAction();
		}
	}
}