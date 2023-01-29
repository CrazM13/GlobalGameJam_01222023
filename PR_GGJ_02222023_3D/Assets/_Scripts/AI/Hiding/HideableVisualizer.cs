using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableVisualizer : MonoBehaviour {

	[SerializeField] private new MeshRenderer renderer;

	void Update() {
		bool isHidden = ServiceLocator.BushManager.IsInBush(transform.position);

		renderer.material.color = isHidden ? Color.grey : Color.white;
	}
}
