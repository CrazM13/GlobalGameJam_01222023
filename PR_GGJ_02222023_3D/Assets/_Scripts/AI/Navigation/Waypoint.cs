using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {



	void Awake() {
		ServiceLocator.Pathfinder.AddWaypoint(transform.position);
	}

}
