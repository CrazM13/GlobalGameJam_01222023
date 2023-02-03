using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVignetteController : MonoBehaviour {

	[SerializeField] private CameraVignette vignette;
	[SerializeField] private float dangerDistance;

	private List<Transform> dangers = new List<Transform>();

	private bool isEffectActive = false;

	// Start is called before the first frame update
	void Start() {
		vignette.IsEffectActive = false;

		foreach (AIEnemy enemy in FindObjectsOfType<AIEnemy>()) {
			dangers.Add(enemy.transform);
		}
	}

	// Update is called once per frame
	void Update() {
		float distanceToDanger = GetNearestTargetDanger(transform.position);

		if (distanceToDanger < dangerDistance) {
			if (!isEffectActive) vignette.IsEffectActive = isEffectActive = true;
			vignette.Strength = 2 - (distanceToDanger / dangerDistance);
			vignette.Scale = 1 - (distanceToDanger / dangerDistance);


		} else if (isEffectActive) {
			vignette.IsEffectActive = isEffectActive = false;
		}
	}

	private float GetNearestTargetDanger(Vector3 startPosition) {

		float closestDistance = float.MaxValue;

		foreach (Transform danger in dangers) {
			if (!danger) continue;

			float newDistance = Vector3.Distance(startPosition, danger.position);
			if (newDistance < closestDistance) {
				closestDistance = newDistance;
			}
		}

		return closestDistance;
	}

	private void OnDestroy() {
		vignette.IsEffectActive = false;
		vignette.Strength = 1;
		vignette.Scale = 1;
	}
}
