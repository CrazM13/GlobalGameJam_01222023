using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

	[SerializeField] private GameObject respawnAreaPrefab;

	private GameObject currentRespawnArea = null;

	void Start() {
		ServiceLocator.CatchingManager.SetRespawnPoint(this);
	}

	private void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position + new Vector3(3.5f, 0, 0), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(-3.5f, 0, 0), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(0, 0, 3.5f), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(0, 0, -3.5f), new Vector3(0.25f, 1, 0.25f));

		Gizmos.DrawCube(transform.position + new Vector3(2.5f, 0, -2.5f), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(-2.5f, 0, 2.5f), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(-2.5f, 0, -2.5f), new Vector3(0.25f, 1, 0.25f));
		Gizmos.DrawCube(transform.position + new Vector3(2.5f, 0, 2.5f), new Vector3(0.25f, 1, 0.25f));
	}

	public void RespawnPlayer() {
		ServiceLocator.Player.transform.parent.position = transform.position;
		Physics.SyncTransforms();

		if (currentRespawnArea) Destroy(currentRespawnArea);

		currentRespawnArea = Instantiate(respawnAreaPrefab, transform);
	}

}
