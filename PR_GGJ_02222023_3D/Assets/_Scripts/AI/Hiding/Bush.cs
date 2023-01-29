using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour {

	void Awake() {
		ServiceLocator.BushManager.AddBush(this);
	}

	public bool IsInBush(Vector3 point) {
		Vector2 normalizedPoint = NormalizePoint(point);
		Vector3 scale = transform.localScale * 0.5f;
		float distanceToCenter = ((normalizedPoint.x * normalizedPoint.x) / (scale.x * scale.x)) + ((normalizedPoint.y * normalizedPoint.y) / (scale.z * scale.z));

		Debug.Log($"{normalizedPoint} | {distanceToCenter}");

		return distanceToCenter <= 1;
	}

	private Vector2 NormalizePoint(Vector3 position) {
		return new Vector2(position.x - transform.position.x, position.z - transform.position.z);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawLine(transform.position - transform.forward * transform.localScale.z, transform.position + transform.forward * transform.localScale.z);
		Gizmos.DrawLine(transform.position - transform.right * transform.localScale.x, transform.position + transform.right * transform.localScale.x);
	}

}
