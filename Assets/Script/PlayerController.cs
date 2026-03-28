// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     private Animator anim;
//     public float walkSpeed = 5.0f;
//     public float rotationSpeed = 720f;

//     [Range(0, 1)] 
//     public float currentWeight = 0f; 

//     void Start()
//     {
//         anim = GetComponent<Animator>();
//     }


//     void Update()
// {
//     float moveForward = Input.GetAxis("Vertical"); 
//     float moveSide = Input.GetAxis("Horizontal");

//     Vector3 camForward = Camera.main.transform.forward;
//     Vector3 camRight = Camera.main.transform.right;

//     camForward.y = 0;
//     camRight.y = 0;

//     camForward = camForward.normalized; 
//     camRight = camRight.normalized;     

//     Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

//     if (direction.magnitude >= 0.1f)
//     {
//         direction.y = 0; 

//         Quaternion targetRotation = Quaternion.LookRotation(direction);
//         transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

//         transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
//     }

//     if (anim != null)
//         anim.SetFloat("Speed", direction.magnitude);
// }
// }







using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    public float walkSpeed = 5.0f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5.0f; // Force applied upward
    
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Movement Input
        float moveForward = Input.GetAxis("Vertical"); 
        float moveSide = Input.GetAxis("Horizontal");

        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

        // 2. Rotate
        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 3. Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (anim != null) anim.SetTrigger("Jump");
        }

        // 4. Animation
        if (anim != null) anim.SetFloat("Speed", direction.magnitude);
    }

    // Physics movement should happen in FixedUpdate
    void FixedUpdate()
    {
        float moveForward = Input.GetAxis("Vertical"); 
        float moveSide = Input.GetAxis("Horizontal");
        isGrounded = Physics.CheckSphere(transform.position, 0.1f);
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

        // Move the Rigidbody position
        rb.MovePosition(rb.position + direction * walkSpeed * Time.fixedDeltaTime);
    }

    // Simple Ground Check
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}