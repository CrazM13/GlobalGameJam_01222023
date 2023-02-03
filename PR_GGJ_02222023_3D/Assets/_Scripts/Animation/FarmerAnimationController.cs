using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimationController : MonoBehaviour {

	[SerializeField] private Animator farmerAnimController;
	[SerializeField] private AIEnemy farmer;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		farmerAnimController.SetBool("IsMoving", farmer.IsMoving);
	}
}
