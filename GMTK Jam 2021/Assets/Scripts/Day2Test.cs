using UnityEngine;

public class Day2Test : MonoBehaviour
{
    CharacterController _character;
    Animator _animator;
    [SerializeField] float speed = 0f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    public float _rotationSpeed = 180;
    private Vector3 rotation;
    private Vector3 velocity;

    //[SerializeField] bool isGrounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        Debug.Log(speed);    
    
    }

    void Movement()
    {
        //isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (velocity.y < 0) //isGrounded &&
        {
            velocity.y = -2f;
        }
        rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * _rotationSpeed * Time.deltaTime, 0);
        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        move = this.transform.TransformDirection(move * speed);
               
        //if (isGrounded)
        //{
            if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (move == Vector3.zero)
            {
                Idle();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Jump();
            }
            move *= speed;
       // }

        _character.Move(move );
        this.transform.Rotate(this.rotation);
        velocity.y += gravity * Time.deltaTime;
        _character.Move(velocity * Time.deltaTime);
    }


    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
        _animator.SetTrigger("jumpTrigger");

    }
    void Walk()
    {
        speed = walkSpeed;
        _animator.SetFloat("_speed", 0.5f, 0.1f, Time.deltaTime);

    }
    void Run()
    {
        speed = runSpeed;
        _animator.SetFloat("_speed", 1f, 0.1f, Time.deltaTime);
    }
    void Idle()
    {
        _animator.SetFloat("_speed", 0, 0.1f, Time.deltaTime);
    }
}
