using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibbleController : MonoBehaviour {

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, float.MaxValue, -1, QueryTriggerInteraction.Collide)) {
				Nibbleable_Object nibbleable = hit.collider.GetComponent<Nibbleable_Object>();

				if (nibbleable) {
					nibbleable.gettingNibbled(100);
				}
			}
		}
	}
}
