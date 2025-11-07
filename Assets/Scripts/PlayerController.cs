using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool isOcuped = false;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float gravity = -9.81f;
    public float standingHeight = 2f;
    public float crouchingHeight = 1f;
    public bool isHidden = false;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float yVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isOcuped) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Transform cam = Camera.main.transform;
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * v + camRight * h;
            move.Normalize();

            bool isCrouching = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C);
            controller.height = isCrouching ? crouchingHeight : standingHeight;
            controller.center = new Vector3(0, controller.height / 2f, 0);

            float currentSpeed = walkSpeed;
            if (isCrouching)
                currentSpeed = crouchSpeed;
            else if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = runSpeed;

            moveDirection = move * currentSpeed;

            if (controller.isGrounded && yVelocity < 0)
            {
                yVelocity = -2f;
            }
            else
            {
                yVelocity += gravity * Time.deltaTime;
            }

            moveDirection.y = yVelocity;

            controller.Move(moveDirection * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HideSpot"))
            isHidden = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HideSpot"))
            isHidden = false;
    }
}










