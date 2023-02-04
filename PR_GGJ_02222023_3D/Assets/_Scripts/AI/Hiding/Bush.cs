using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour {

	private const string CUTOUT_ID = "_IsCutoutActive";

	private new MeshRenderer renderer;

	private bool isCutoutShown = false;

	void Awake() {
		ServiceLocator.BushManager.AddBush(this);

		renderer = GetComponentInChildren<MeshRenderer>();
	}

	public bool IsInBush(Vector3 point) {
		Vector2 normalizedPoint = NormalizePoint(point);
		Vector3 scale = transform.localScale * 0.5f;
		float distanceToCenter = ((normalizedPoint.x * normalizedPoint.x) / (scale.x * scale.x)) + ((normalizedPoint.y * normalizedPoint.y) / (scale.z * scale.z));

		return distanceToCenter <= 1;
	}

	private Vector2 NormalizePoint(Vector3 position) {
		return new Vector2(position.x - transform.position.x, position.z - transform.position.z);
	}

	public void ShowCutout(bool shouldShow) {
		if (isCutoutShown != shouldShow) {
			isCutoutShown = shouldShow;

			if (renderer) {
				foreach (Material mat in renderer.materials) {
					mat.SetInt(CUTOUT_ID, shouldShow ? 1 : 0);
				}
			}

		}
	}

}
