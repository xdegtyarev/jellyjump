using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
	[SerializeField] float speed;
	private bool _drowned = true;
	private Transform _cachedTransform;

	public void Reset() {
		_drowned = false;
		_cachedTransform.position = Vector3.zero;
	}

	void Awake() {
		_cachedTransform = transform;
	}

	void OnEnable() {
		JellyControl.drowned += OnJellyDrowned;
	}

	void OnDisable() {
		JellyControl.drowned -= OnJellyDrowned;
	}

	void OnJellyDrowned() {
		_drowned = true;
	}

	void Update() {
		if (!_drowned) {
			_cachedTransform.position += Vector3.up * speed * Time.deltaTime;
		}
	}
}
