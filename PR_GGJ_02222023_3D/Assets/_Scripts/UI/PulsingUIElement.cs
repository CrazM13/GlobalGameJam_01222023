using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PulsingUIElement : MonoBehaviour {

	[SerializeField] private AnimationCurve pulse;
	[SerializeField] private float strength;
	[SerializeField] private float speed;
	[SerializeField] private float offset;

	private Vector3 scale;

	private bool isPlaying = true;

	public float Speed {
		get => speed;
		set => speed = value;
	}

	private float timer;

	// Start is called before the first frame update
	void Start() {
		timer = offset % 1;
		scale = transform.localScale;
	}

	// Update is called once per frame
	void Update() {
		if (isPlaying) {
			timer = (timer + (Time.deltaTime * Speed)) % 1;

			transform.localScale = scale * (1 + (pulse.Evaluate(timer) * strength));
		} else if (timer != 0) {
			timer = 0;
			transform.localScale = scale * (1 + (pulse.Evaluate(timer) * strength));
		}
	}

	public void Play() {
		isPlaying = true;
	}

	public void Stop() {
		isPlaying = false;
	}
}
