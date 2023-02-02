using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingManager {

	private int maxLives;

	private int currentLife;

	private RespawnPoint respawnPoint;

	public CatchingManager(int maxLives) {
		this.maxLives = maxLives;
	}

	public void CatchRabbit() {
		currentLife++;
		if (respawnPoint && currentLife < maxLives) {
			Respawn();
		} else {
			ServiceLocator.SceneManager.LoadSceneByName("LoseScene");
		}
	}

	public void SetRespawnPoint(RespawnPoint respawnPoint) {
		this.respawnPoint = respawnPoint;
	}

	private void Respawn() {
		respawnPoint.RespawnPlayer();
	}
	
}
