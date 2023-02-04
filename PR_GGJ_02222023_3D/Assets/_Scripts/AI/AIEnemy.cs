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
	[SerializeField] private AIVision vision;

	[Header("Capture Settings")]
	[SerializeField] private float captureDistance;
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

	private float stunDuration = 0;
	private float timeUntilAction = 0;

	private float ActionSpeed => 1f / actionsPerSecond;

	public bool IsMoving => path != null;
	public bool IsStunned => stunDuration > 0;

	// Start is called before the first frame update
	void Awake() {
		SetState(AIStates.IDLE);
		timeUntilAction = ActionSpeed;

		Nibbleable_Object nibbleObject = GetComponent<Nibbleable_Object>();
		nibbleObject.OnNibbleStart.AddListener(OnNibble);
		nibbleObject.OnNibbleContinue.AddListener(OnNibble);
	}

	// Update is called once per frame
	void Update() {

		if (stunDuration <= 0) timeUntilAction -= Time.deltaTime;
		else stunDuration -= Time.deltaTime;

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
		if (ServiceLocator.Player && vision.CanSeePoint(transform.position, transform.forward, ServiceLocator.Player.transform.position)) {
			SetState(AIStates.CHASE);
			return;
		}

		if (timeUntilAction <= 0) {
			SetState(AIStates.PATROL);
			MoveToRandomWaypoint();
		}
	}

	private void OnStatePatrol() {

		if (ServiceLocator.Player && vision.CanSeePoint(transform.position, transform.forward, ServiceLocator.Player.transform.position)) {
			SetState(AIStates.CHASE);
			path = null;
			return;
		}

		if (path != null && path.corners.Length > pathIndex) {

			if (Vector3.Distance(transform.position, path.corners[pathIndex]) < targetWaypointDistance) {
				pathIndex++;
				if (path.corners.Length <= pathIndex) {
					pathIndex = 1;
					path = null;
					SetState(AIStates.IDLE);
				}
			} else {
				transform.LookAt(Flatten(path.corners[pathIndex]), Vector3.up);
				
				Vector3 newPositon = Vector3.MoveTowards(transform.position, path.corners[pathIndex], movementSpeed * Time.deltaTime);
				if (NavMesh.SamplePosition(newPositon, out NavMeshHit hit, 1, -1)) {
					newPositon = hit.position;
				}
				transform.position = newPositon;
			}
		} else {
			SetState(AIStates.IDLE);
		}
	}

	private void OnStateChase() {
		if (!ServiceLocator.Player) SetState(AIStates.IDLE);

		targetPosition = ServiceLocator.Player.transform.position;

		if (Vector3.Distance(targetPosition, transform.position) < captureDistance) {
			ServiceLocator.CatchingManager.CatchRabbit();
		}

		if (path != null && path.corners.Length > pathIndex) {

			if (Vector3.Distance(transform.position, path.corners[pathIndex]) < targetWaypointDistance) {
				if (vision.CanSeePoint(transform.position, transform.forward, ServiceLocator.Player.transform.position)) {
					MoveToTarget();
				} else {
					SetState(AIStates.IDLE);
				}
			} else {
				transform.LookAt(Flatten(path.corners[pathIndex]), Vector3.up);
				transform.position = Vector3.MoveTowards(transform.position, path.corners[pathIndex], chaseSpeed * Time.deltaTime);
			}
		} else {
			MoveToTarget();
		}
	}

	private void MoveToRandomWaypoint() {
		// Set Random Waypoint
		targetPosition = ServiceLocator.Pathfinder.GetRandomWaypoint();
		MoveToTarget();
	}

	private void MoveToTarget() {
		// Calculate Path
		path = ServiceLocator.Pathfinder.GetPathToPosition(transform.position, targetPosition);

		//if (path.status == NavMeshPathStatus.PathInvalid) path = null;
		pathIndex = 1;
	}

	private Vector3 Flatten(Vector3 position) {
		return new Vector3(position.x, transform.position.y, position.z);
	}

	public void SetState(AIStates newState) {
		OnStateChange.Invoke(newState);
		currentState = newState;
	}

	private void Stun(float duration) {
		if (currentState != AIStates.IDLE) SetState(AIStates.IDLE);
		stunDuration = duration;
	}

	private void OnNibble() {
		Debug.Log("NIBBLED");
		Stun(5f);

		GetComponent<Nibbleable_Object>().resetFarmerHP();
	}
	
	private void OnDrawGizmos() {
		Color savedColour = Gizmos.color;
		{
			Vector3 newPositiveFOVPoint = transform.forward * vision.MaxDistance;
			newPositiveFOVPoint = Quaternion.AngleAxis(vision.FOV * 0.5f, Vector3.up) * newPositiveFOVPoint;
			newPositiveFOVPoint += transform.position;

			Vector3 newNegativeFOVPoint = transform.forward * vision.MaxDistance;
			newNegativeFOVPoint = Quaternion.AngleAxis(vision.FOV * -0.5f, Vector3.up) * newNegativeFOVPoint;
			newNegativeFOVPoint += transform.position;

			Vector3 newDistanceFOVPoint = transform.forward * vision.MaxDistance;
			newDistanceFOVPoint += transform.position;

			Gizmos.DrawLine(transform.position, newPositiveFOVPoint);
			Gizmos.DrawLine(transform.position, newNegativeFOVPoint);
			Gizmos.DrawLine(newPositiveFOVPoint, newDistanceFOVPoint);
			Gizmos.DrawLine(newNegativeFOVPoint, newDistanceFOVPoint);
		}

		{
			Gizmos.color = Color.green;
			if (path != null) {
				for (int i = 1; i < path.corners.Length; i++) {
					Gizmos.DrawLine(path.corners[i - 1], path.corners[i]);
				}
			}
		}

		Gizmos.color = savedColour;
	}

}
