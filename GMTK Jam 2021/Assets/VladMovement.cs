using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VladMovement : MonoBehaviour
{
    CharacterController controller;
    public Transform skeleton;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    Animator animator;
    void Start()

    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5f, 0, Input.GetAxis("Vertical") * Time.deltaTime * 5f);
        float movementMagnitude = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), 0, Mathf.Abs(Input.GetAxis("Vertical")));

        Debug.Log(movementMagnitude);

        controller.Move(movement);

        if (movementMagnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                animator.SetBool("isGrounded", false);
            }
        }

        else
        {
            animator.SetBool("isGrounded", false);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
