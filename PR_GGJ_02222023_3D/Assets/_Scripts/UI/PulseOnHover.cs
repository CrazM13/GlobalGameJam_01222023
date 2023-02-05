using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PulseOnHover : PulsingUIElement, IPointerEnterHandler, IPointerExitHandler {

	private void Awake() {
		Stop();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		Play();
	}

	public void OnPointerExit(PointerEventData eventData) {
		Stop();
	}
}
