using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 6f;
    public float jump = 2.5f;
    float turnSmoothVelocity;

    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    public float jumpCooldown = 3f;

    private Vector3 velocity;
    private bool isGrounded; 

    public float turnSmoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~groundMask);

        if(controller.isGrounded)
        {
            velocity.y = -0.5f;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jump * -2.0f * gravity);
                isGrounded = false;  
            }

            StartCooldown();
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");       

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir* speed * Time.deltaTime);

        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public IEnumerable StartCooldown()
    {
        isGrounded = false;
        yield return new WaitForSeconds(jumpCooldown);
        isGrounded = true;
    }
}
 