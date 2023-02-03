using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnNibble : MonoBehaviour {

	[SerializeField] private AnimationCurve rotationOverTime;
	[SerializeField] private float speed = 1;
	[SerializeField] private float scale = 1;

	private float playTime = -1;

	private Vector3 startEulers;

	// Start is called before the first frame update
	void Start() {
		// Hook into event

		startEulers = transform.rotation.eulerAngles;
	}

	// Update is called once per frame
	void Update() {
		if (playTime >= 0) {
			playTime += Time.deltaTime * speed;

			transform.rotation = Quaternion.Euler(startEulers.x + rotationOverTime.Evaluate(playTime) * scale, startEulers.y, startEulers.z);

			if (playTime > 1) Stop();
		}
	}

	private void Stop() {
		playTime = -1;
		transform.rotation = Quaternion.Euler(startEulers);
	}

	[ContextMenu("Play")]
	private void Play() {
		playTime = 0;
	}

}
