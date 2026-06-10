using UnityEngine;

public class PlayerStopOnTouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopObject"))
        {
            if (playerMovement != null)
            {
                playerMovement.enabled = false;    
                Debug.Log("Player touched StopObject, movement stopped!");
            }
        }
    }
}
