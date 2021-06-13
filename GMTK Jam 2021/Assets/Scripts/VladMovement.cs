using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    public int keyCounter = 0;
    public bool isGrounded;
    Animator animator;
    public GameObject ball;
    Rigidbody ballRb;
    Rigidbody playerRB;
    Vector3 angularVelocity;
    [SerializeField] SoundEffectManagerScript soundEffects;

    public TMP_Text keyCounterText;

    void Start()

    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ballRb = ball.GetComponent<Rigidbody>();
        playerRB = GetComponent<Rigidbody>();
        angularVelocity = playerRB.angularVelocity;
        soundEffects = FindObjectOfType<SoundEffectManagerScript>();

        //ball is 6.38 metres away

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

        //Debug.Log(velocity.y);

        animator.SetBool("isGrounded", isGrounded);

        //Debug.Log(isGrounded);

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Rotate(0f, 720f * Time.deltaTime, 0f, Space.Self);
            //ball.transform.Rotate(0f, 360f * Time.deltaTime, 0f, Space.World);
            Vector3 direction;
            direction = transform.position - ball.transform.position;
            //ballRb.AddForce(-transform.right * 100f, ForceMode.Force);
            ball.transform.rotation *= Quaternion.Euler(transform.InverseTransformVector(angularVelocity) * Mathf.Rad2Deg * Time.fixedDeltaTime);

        }
        if (transform.position.y < -10f) //if you fall off
        {
            transform.position = new Vector3(-70.6f, 22, 267.8f); //spawn at starting position
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        UpdateKeyCounter();
    }

    public void UpdateKeyCounter()
    {
        keyCounterText.text = "Keys: " + keyCounter + "/5";
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, groundDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Key")
        {
            soundEffects.Key();
            keyCounter++;
            Destroy(other.gameObject);
        }
    }
}