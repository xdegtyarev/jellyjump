using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelControl : MonoBehaviour {
	public static event Action<int> nextObstacleReached;
	public enum LevelBlockType {
		Moving,
		Rotating
	}

	[System.Serializable]
	public class LevelBlock {
		public float distance;
		public LevelBlockType blockType;
		public float activationDelay = 1f;
		public float activationSpeed = 1f;
	}

	[SerializeField] LevelBlock[] level;
	[SerializeField] int prefetchCount = 10;
	[SerializeField] GameObject[] obstacleBase;

	private int currentBlockId;
	private int prevIdx;
	private float currentY;

	private Transform _cachedTransform;
	private List<Obstacle> _prevChunk = new List<Obstacle>();
	private List<Obstacle> _currentChunk = new List<Obstacle>();

	void Awake() {
		_cachedTransform = transform;
	}

	void OnEnable() {
		JellyControl.landedOnPlatform += OnPlatformLanded;
	}

	void OnDisable() {
		JellyControl.landedOnPlatform -= OnPlatformLanded;
	}

	void OnPlatformLanded(Obstacle obstacle) {
		var idx = _currentChunk.IndexOf(obstacle);
		if (idx > prevIdx) {
			if (prevIdx == 0 && _prevChunk.Count > 0) {
				//clear prev chunk when jumped to new one
				ClearPrevChunk();
			}
			//in case you can jump over more than one
			//(i found it possible on trampoline in orig game)
			//but it'll require to rework activation of obstacles (with triggers probably)
			currentBlockId += (idx - prevIdx);
			nextObstacleReached(currentBlockId);
			prevIdx = idx;

			var nextIdx = idx + 1;
			if (nextIdx == _currentChunk.Count) {
				//last block reached
				SpawnNextChunk();
				nextIdx = 0;
				prevIdx = -1;
			}
			//only once
			_currentChunk[nextIdx].Activate();
		}
	}

	public void Reset() {
		currentBlockId = 0;
		prevIdx = -1;
		currentY = 0f;
		ClearPrevChunk();
		ClearCurrentChunk();
		SpawnNextChunk();
		_currentChunk[0].Activate();
	}

	void ClearCurrentChunk() {
		while (_currentChunk.Count > 0) {
			//TODO: return to pool
			GameObject.Destroy(_currentChunk[0].gameObject);
			_currentChunk.RemoveAt(0);
		}
		_currentChunk.Clear();
	}

	void ClearPrevChunk() {
		while (_prevChunk.Count > 0) {
			//TODO: return to pool
			GameObject.Destroy(_prevChunk[0].gameObject);
			_prevChunk.RemoveAt(0);
		}
		_prevChunk.Clear();
	}

	public void SpawnNextChunk() {
		_prevChunk = new List<Obstacle>(_currentChunk);
		_currentChunk.Clear();
		for (int i = currentBlockId; i < currentBlockId + prefetchCount; i++) {
			var nextObstacleData = level[i % level.Length];
			currentY += nextObstacleData.distance;
			var go = GameObject.Instantiate(
				obstacleBase[(int)nextObstacleData.blockType],
				Vector3.up * currentY,
				Quaternion.identity,
				_cachedTransform
			);
			var obstacle = go.GetComponent<Obstacle>();
			obstacle.Setup(nextObstacleData.activationDelay, nextObstacleData.activationSpeed);
			_currentChunk.Add(obstacle);
		}
	}
}
