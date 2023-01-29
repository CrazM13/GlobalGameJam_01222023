using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIVision {

	[SerializeField] private float fov = 90;
	[SerializeField] private float maxDistance = 10;

	public bool CanSeePoint(Vector3 position, Vector3 forward, Vector3 point) {
		Vector3 direction = point - position;

		float angle = Vector3.Angle(direction, forward);
		float distance = Vector3.Distance(position, point);

		return distance < maxDistance && angle < fov * 0.5f && angle > fov * -0.5f;
	}

	public float FOV => fov;
	public float MaxDistance => maxDistance;

}
