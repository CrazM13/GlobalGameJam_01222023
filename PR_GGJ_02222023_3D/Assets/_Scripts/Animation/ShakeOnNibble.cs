using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnNibble : MonoBehaviour {

	[SerializeField] private AnimationCurve rotationOverTime;
	[SerializeField] private float speed = 1;
	[SerializeField] private float scale = 1;

	private float playTime = -1;

	// Start is called before the first frame update
	void Start() {
		// Hook into event
	}

	// Update is called once per frame
	void Update() {
		if (playTime >= 0) {
			playTime += Time.deltaTime * speed;

			transform.rotation = Quaternion.Euler(rotationOverTime.Evaluate(playTime) * scale, 0, 0);

			if (playTime > 1) Stop();
		}
	}

	private void Stop() {
		playTime = -1;
		transform.rotation = Quaternion.identity;
	}

	[ContextMenu("Play")]
	private void Play() {
		playTime = 0;
	}

}
