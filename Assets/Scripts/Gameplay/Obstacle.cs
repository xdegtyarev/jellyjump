using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	private Animator _cachedAnimator;
	private float _speed;
	private float _delay;

	void Awake() {
		_cachedAnimator = GetComponent<Animator>();
	}

	IEnumerator ActivationCoroutine() {
		yield return new WaitForSeconds(_delay);
		_cachedAnimator.speed = _speed;
		_cachedAnimator.SetTrigger("Activate");
		yield return null;
	}

	public void Setup(float delay, float speed){
		_delay = delay;
		_speed = speed;
	}

	public void Activate() {
		StartCoroutine(ActivationCoroutine());
	}
}