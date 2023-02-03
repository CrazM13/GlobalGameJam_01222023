using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatchingManager {

	private int maxLives;

	private int currentLife;

	private RespawnPoint respawnPoint;

	private float Completion => 1 - ((float) ServiceLocator.PlantManager.remainingPlantCount() / ServiceLocator.PlantManager.totalPlantCount());

	public bool IsLastLife => currentLife >= maxLives || Completion >= 0.75f;

	public int RemainingLives => maxLives - currentLife;
	public int CurrentLife => currentLife;

	public UnityEvent<CatchingManager> OnPlayerCaught { get; set; } = new UnityEvent<CatchingManager>();

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

		OnPlayerCaught.Invoke(this);
	}

	public void SetRespawnPoint(RespawnPoint respawnPoint) {
		this.respawnPoint = respawnPoint;
	}

	private void Respawn() {
		respawnPoint.RespawnPlayer();
	}
	
}
