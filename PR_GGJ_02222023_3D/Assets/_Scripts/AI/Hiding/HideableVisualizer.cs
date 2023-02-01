using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableVisualizer : MonoBehaviour {

	private bool isHidden = false;

	void Update() {
		bool newIsHidden = ServiceLocator.BushManager.IsInBush(transform.position);

		if (newIsHidden != isHidden) {
			ServiceLocator.BushManager.UpdateCutoutsOnBushes(transform.position);
			isHidden = newIsHidden;
		}
	}
}
