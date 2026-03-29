using UnityEngine;

public class StickyTrash : MonoBehaviour
{
    private bool isStuck = false;
    private Rigidbody trashRb;

    void Start()
    {
        trashRb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isStuck)
        {
            StickToPlayer(collision.gameObject);
        }
    }

    void StickToPlayer(GameObject player)
    {
        isStuck = true;

        transform.SetParent(player.transform);

        if (trashRb != null)
        {
            trashRb.isKinematic = true; 
            trashRb.useGravity = false;
        }


        GetComponent<Collider>().enabled = false;

        Debug.Log("Trash Stuck!");
        
   
    }
}