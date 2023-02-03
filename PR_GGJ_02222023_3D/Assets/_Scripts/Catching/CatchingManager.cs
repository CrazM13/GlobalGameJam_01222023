using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingManager {

	private int maxLives;

	private int currentLife;

	private RespawnPoint respawnPoint;

	private float Completion => 1 - ((float) ServiceLocator.PlantManager.remainingPlantCount() / ServiceLocator.PlantManager.totalPlantCount());

	public bool IsLastLife => currentLife >= maxLives && Completion >= 0.75f;

	public CatchingManager(int maxLives) {
		this.maxLives = maxLives;
	}

	public void CatchRabbit() {
		currentLife++;
		if (respawnPoint && !IsLastLife) {
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
