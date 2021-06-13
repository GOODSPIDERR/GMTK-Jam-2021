using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VladMovement : MonoBehaviour
{
    CharacterController controller;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float movementSpeed = 5f;
    Vector3 velocity;
    public bool isGrounded;
    Animator animator;
    void Start()

    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
    }
    void Update()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Debug.Log(velocity.y);

        animator.SetBool("isGrounded", isGrounded);

        Debug.Log(isGrounded);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        float movementMagnitude = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), 0, Mathf.Abs(Input.GetAxis("Vertical")));

        //Debug.Log(movementMagnitude);

        controller.Move(movement);

        if (movementMagnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 0.5f);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }



        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, groundDistance);
    }
}
