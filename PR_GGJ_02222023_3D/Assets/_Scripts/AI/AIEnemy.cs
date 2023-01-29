using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIEnemy : MonoBehaviour {

	#region Inspector
	[Header("Movement Settings")]
	[SerializeField] private float movementSpeed;
	[SerializeField] private float chaseSpeed;
	[SerializeField] private float targetWaypointDistance;

	[Header("AI Settings")]
	[SerializeField] private float actionsPerSecond;
	#endregion

	#region Events
	/// <summary>
	/// Triggers prior to the AI state changes. Passed value is the new state
	/// </summary>
	public UnityEvent<AIStates> OnStateChange { get; private set; } = new UnityEvent<AIStates>();
	#endregion

	private AIStates currentState = AIStates.IDLE;
	private Vector3 targetPosition;

	private NavMeshPath path;
	private int pathIndex;

	private float timeUntilAction = 0;

	private float ActionSpeed => 1f / actionsPerSecond;


	// Start is called before the first frame update
	void Awake() {
		SetState(AIStates.IDLE);
		timeUntilAction = ActionSpeed;
	}

	// Update is called once per frame
	void Update() {

		timeUntilAction -= Time.deltaTime;

		switch (currentState) {
			case AIStates.IDLE:
				OnStateIdle();
				break;
			case AIStates.PATROL:
				OnStatePatrol();
				break;
			case AIStates.CHASE:
				OnStateChase();
				break;
		}

		if (timeUntilAction <= 0) {
			timeUntilAction += ActionSpeed;
		}
	}

	private void OnStateIdle() {
		if (timeUntilAction <= 0) {
			SetState(AIStates.PATROL);
			MoveToRandomWaypoint();
		}
	}

	private void OnStatePatrol() {
		if (path != null && path.corners.Length > pathIndex) {

			//targetPosition = path.corners[pathIndex];

			if (Vector3.Distance(transform.position, path.corners[pathIndex]) < targetWaypointDistance) {
				pathIndex++;
				if (path.corners.Length <= pathIndex) {
					pathIndex = 1;
					path = null;
					SetState(AIStates.IDLE);
				}
			} else {
				transform.LookAt(Flatten(path.corners[pathIndex]), Vector3.up);
				transform.position = Vector3.MoveTowards(transform.position, path.corners[pathIndex], movementSpeed * Time.deltaTime);
			}
		} else {
			SetState(AIStates.IDLE);
		}
	}

	private void OnStateChase() {
		
	}

	private void MoveToRandomWaypoint() {
		// Set Random Waypoint
		targetPosition = ServiceLocator.Pathfinder.GetRandomWaypoint();
		// Calculate Path
		path = ServiceLocator.Pathfinder.GetPathToPosition(transform.position, targetPosition);

		//if (path.status == NavMeshPathStatus.PathInvalid) path = null;

		pathIndex = 0;
	}

	private Vector3 Flatten(Vector3 position) {
		return new Vector3(position.x, transform.position.y, position.z);
	}

	public void SetState(AIStates newState) {
		OnStateChange.Invoke(newState);
		currentState = newState;
	}

}
