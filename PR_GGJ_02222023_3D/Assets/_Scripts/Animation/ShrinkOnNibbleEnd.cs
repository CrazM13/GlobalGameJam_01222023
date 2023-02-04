using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkOnNibbleEnd : MonoBehaviour {

	[SerializeField] private AnimationCurve scaleOverTime;
	[SerializeField] private float speed = 1;
	[SerializeField] private float strengh = 1;

	private float playTime = -1;

	private Vector3 startScale;

	// Start is called before the first frame update
	void Start() {
		// Hook into event
		Nibbleable_Object nibbleableObject = GetComponent<Nibbleable_Object>();
		nibbleableObject.OnNibbleEnd.AddListener(Play);

		startScale = transform.localScale;
	}

	// Update is called once per frame
	void Update() {
		if (playTime >= 0) {
			playTime += Time.deltaTime * speed;

			transform.localScale = startScale * (scaleOverTime.Evaluate(playTime) * strengh);

			if (playTime > 1) Stop();
		}
	}

	private void Stop() {
		playTime = -1;
	}

	[ContextMenu("Play")]
	private void Play() {
		playTime = 0;
	}

}