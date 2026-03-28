using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    public float walkSpeed = 5.0f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5.0f; 
    
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveForward = Input.GetAxis("Vertical"); 
        float moveSide = Input.GetAxis("Horizontal");

        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (anim != null) anim.SetTrigger("Jump");
        }

        if (anim != null) anim.SetFloat("Speed", direction.magnitude);
    }

    void FixedUpdate()
    {
        float moveForward = Input.GetAxis("Vertical"); 
        float moveSide = Input.GetAxis("Horizontal");
        isGrounded = Physics.CheckSphere(transform.position, 0.1f);
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

        rb.MovePosition(rb.position + direction * walkSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}