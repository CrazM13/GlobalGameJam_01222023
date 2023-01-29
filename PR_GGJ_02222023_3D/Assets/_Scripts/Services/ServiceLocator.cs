using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static SceneTransition @SceneManager { get; set; }
	public static AIPather Pathfinder { get; set; }
	public static BushManager @BushManager { get; set; }
	public static ThirdPersonMovement Player { get; set; }

	// Singleton
	private static ServiceLocator instance;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		LocateServices();
	}

	private void LocateServices() {
		@SceneManager = FindObjectOfType<SceneTransition>();
		Pathfinder = new AIPather();
		@BushManager = new BushManager();
		Player = FindObjectOfType<ThirdPersonMovement>();
	}

	private void OnDestroy() {
		if (instance == this) {
			@SceneManager = null;
			Pathfinder = null;
			@BushManager = null;
			Player = null;
		}
	}
}
