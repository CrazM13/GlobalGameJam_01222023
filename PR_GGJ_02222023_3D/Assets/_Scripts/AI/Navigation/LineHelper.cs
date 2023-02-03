using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LineHelper : MonoBehaviour {

	[SerializeField] private GameObject arrowPrefab;
	[SerializeField] private float updateDelay;
	[SerializeField] private int maxArrowCount;
	[SerializeField] private float spacing;
	[SerializeField] private Vector3 offset;

	private List<GameObject> targets;

	private List<GameObject> arrows = new List<GameObject>();

	float timeUntilUpdate = 0;

	private void Awake() {
		targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Plant"));

		for (int i = 0; i < maxArrowCount; i++) {
			arrows.Add(Instantiate(arrowPrefab, transform));
			arrows[i].SetActive(false);
		}
	}

	private void Update() {
		if (updateDelay > 0) {
			timeUntilUpdate -= Time.deltaTime;
			if (timeUntilUpdate <= 0) {
				timeUntilUpdate = updateDelay;
				UpdatePath();
			}
		}
	}

	private void UpdatePath() {
		Vector3 start = ServiceLocator.Player.transform.position;
		Vector3? end = GetNearestTargetPosition(start);

		if (end.HasValue) {
			SetPath(start, end.Value);
		}
	}

	public void SetPath(Vector3 start, Vector3 end) {

		int arrowIndex = 0;

		foreach (GameObject arrow in arrows) {
			arrow.SetActive(false);
		}

		NavMeshPath path = ServiceLocator.Pathfinder.GetPathToPosition(start, end);

		for (int i = 0; i < path.corners.Length - 1; i++) {
			DrawPathSegment(path, i, ref arrowIndex);
		}

	}

	private void DrawPathSegment(NavMeshPath path, int pathSegment, ref int arrowIndex) {

		Vector3 endPoint = path.corners[pathSegment + 1];

		float distance = Vector3.Distance(path.corners[pathSegment], endPoint);

		int count = Mathf.FloorToInt(distance / spacing);
		float percentage = 1f / count;

		for (int i = 0; i < count; i++) {

			if (arrowIndex < maxArrowCount) {
				arrows[arrowIndex].SetActive(true);

				Vector3 newPosition = Vector3.Lerp(path.corners[pathSegment], endPoint, percentage * i);

				if (NavMesh.SamplePosition(newPosition, out NavMeshHit hit, 1, -1)) {
					newPosition = hit.position;
				}

				arrows[arrowIndex].transform.SetPositionAndRotation(newPosition + offset, Quaternion.LookRotation(endPoint - arrows[arrowIndex].transform.position, Vector3.up));

				arrowIndex++;
			}
		}

	}

	private Vector3? GetNearestTargetPosition(Vector3 startPosition) {

		float closestDistance = float.MaxValue;
		Vector3? closestTargetPosition = null;

		foreach (GameObject target in targets) {
			if (!target) continue;

			float newDistance = Vector3.Distance(startPosition, target.transform.position);
			if (newDistance < closestDistance) {
				closestDistance = newDistance;
				closestTargetPosition = target.transform.position;
			}
		}

		return closestTargetPosition;
	}

}
