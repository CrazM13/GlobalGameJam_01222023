using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour {

	[SerializeField] private Image[] lifeIcons;

	void Start() {
		CatchingManager catchingManager = ServiceLocator.CatchingManager;

		catchingManager.OnPlayerCaught.AddListener(OnPlayerCaught);
		OnPlayerCaught(catchingManager);

		if (lifeIcons.Length > catchingManager.RemainingLives) Debug.LogWarning($"WARNING: More life icons than max lives allowed. Max Lives: {catchingManager.RemainingLives}", gameObject);
	}

	private void OnPlayerCaught(CatchingManager catchingManager) {
		int currentLife = catchingManager.CurrentLife;

		for (int i = 0; i < lifeIcons.Length; i++) {
			lifeIcons[i].color = currentLife <= i ? Color.white : Color.black;
		}
	}
}
