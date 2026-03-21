using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float walkSpeed = 3.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
void Update()
{
    float moveForward = Input.GetAxis("Vertical");
    float moveSide = Input.GetAxis("Horizontal");

    // Send absolute values to animator if you want the 'walking' 
    // animation to play regardless of direction, 
    // OR send the raw values for the 2D Blend Tree to work.
    anim.SetFloat("ForwardSpeed", moveForward);
    anim.SetFloat("SideSpeed", moveSide);

    // Calculate direction based on the character's current facing direction
    Vector3 direction = (transform.forward * moveForward) + (transform.right * moveSide);
    
    if (direction.magnitude > 0.1f)
    {
        transform.position += direction.normalized * walkSpeed * Time.deltaTime;
    }
}
}