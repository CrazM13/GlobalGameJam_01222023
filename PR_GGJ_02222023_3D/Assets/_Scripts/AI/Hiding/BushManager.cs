using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushManager {

	private List<Bush> bushes;

	public BushManager() {
		bushes = new List<Bush>();
	}

	public void AddBush(Bush newBush) {
		bushes.Add(newBush);
	}

	public bool IsInBush(Vector3 point) {
		foreach (Bush bush in bushes) {
			if (bush.IsInBush(point)) return true;
		}

		return false;
	}

	public void UpdateCutoutsOnBushes(Vector3 focusPoint) {
		bool showCutout = IsInBush(focusPoint);
		foreach (Bush bush in bushes) {
			bush.ShowCutout(showCutout);
		}
	}
}
