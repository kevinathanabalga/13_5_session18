using UnityEngine;

public class PlayerStopOnTouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        // Get reference to the PlayerMovement script on the same GameObject
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touched the target GameObject
        if (other.CompareTag("StopObject"))
        {
            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Disable movement script
                Debug.Log("Player touched StopObject, movement stopped!");
            }
        }
    }
}
