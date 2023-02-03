using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAnimationController : MonoBehaviour {

	[SerializeField] private Animator animController;
	[SerializeField] private ThirdPersonMovement playerController;

	// Update is called once per frame
	void Update() {

		bool isMoving = playerController.IsMoving;

		bool isGrounded = playerController.IsGrounded;

		bool isJumping = isGrounded && Input.GetButtonDown("Jump");
		bool isNibbleing = Input.GetButtonDown("Fire1");

		animController.SetBool("IsMoving", isMoving);
		animController.SetBool("IsOnGround", isGrounded);
		if (isJumping) animController.SetTrigger("Jump");
		if (isNibbleing) animController.SetTrigger("Nibble");


	}
}
