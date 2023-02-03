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
		timer = (timer + (Time.deltaTime * Speed)) % 1;

		transform.localScale = scale * (1 + (pulse.Evaluate(timer) * strength));
	}
}
