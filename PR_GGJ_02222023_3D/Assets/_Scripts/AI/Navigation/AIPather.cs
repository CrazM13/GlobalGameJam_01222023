using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIPather {

	private List<Vector3> waypoints;
	private int lastWaypointSelected = -1;

	public AIPather() {
		waypoints = new List<Vector3>();
	}
	
	public void AddWaypoint(Vector3 newWaypoint) {
		waypoints.Add(newWaypoint);
	}

	public Vector3 GetWaypoint(int index) => waypoints[index];

	public Vector3 GetRandomWaypoint() {
		int nextWaypoint = Random.Range(0, waypoints.Count);
		nextWaypoint += (lastWaypointSelected + 1);
		nextWaypoint %= waypoints.Count;

		lastWaypointSelected = nextWaypoint;
		return waypoints[nextWaypoint];
	}

	public NavMeshPath GetPathToPosition(Vector3 startingPosition, Vector3 targetPosition) {
		NavMeshPath path = new NavMeshPath();
		NavMesh.CalculatePath(startingPosition, targetPosition, -1, path);
		return path;
	}

}
