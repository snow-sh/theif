using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float walkSpeed = 5.0f;
    public float rotationSpeed = 720f;

    [Range(0, 1)] 
    public float currentWeight = 0f; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveForward = Input.GetAxis("Vertical"); 
        float moveSide = Input.GetAxis("Horizontal");

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized; 
        camRight = camRight.normalized;     

        Vector3 direction = (camForward * moveForward) + (camRight * moveSide);

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
        }

        if (anim != null)
            anim.SetFloat("Speed", direction.magnitude);
    }
}