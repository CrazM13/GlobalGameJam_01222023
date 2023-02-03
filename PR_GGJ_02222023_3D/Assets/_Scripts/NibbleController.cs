using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibbleController : MonoBehaviour {

	public int dmg;

	public float range = 10f;

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, float.MaxValue, -1, QueryTriggerInteraction.Collide)) {
				if (Vector3.Distance(ServiceLocator.Player.transform.position, hit.point) < range) {
					Nibbleable_Object nibbleable = hit.collider.GetComponent<Nibbleable_Object>();

					if (nibbleable) {
						nibbleable.gettingNibbled(dmg);
					}
				}
			}
		}
	}
}
