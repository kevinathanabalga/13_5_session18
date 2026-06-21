using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Default Respawn")]
    [SerializeField] private Transform defaultRespawnPoint;

    private Vector3 respawnPoint;

    private PlayerPositionHandler positionHandler;
    private Rigidbody2D rb;

    private void Awake()
    {
        positionHandler =
            GetComponent<PlayerPositionHandler>();

        rb =
            GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (defaultRespawnPoint != null)
        {
            respawnPoint =
                defaultRespawnPoint.position;
        }
        else
        {
            Debug.LogWarning(
                "Default Respawn Point belum diassign!"
            );

            respawnPoint =
                transform.position;
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            Respawn();
        }
    }

    public void SetRespawnPoint(
        Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        bool loadedCheckpoint = false;

        if (positionHandler != null)
        {
            Vector3 beforePosition =
                transform.position;

            positionHandler.LoadPosition();

            loadedCheckpoint =
                transform.position != beforePosition;
        }

        // Fallback jika belum ada checkpoint
        if (!loadedCheckpoint)
        {
            transform.position =
                respawnPoint;
        }

        if (rb != null)
        {
            rb.linearVelocity =
                Vector2.zero;

            rb.angularVelocity =
                0f;
        }

        Debug.Log(
            "Player Respawned"
        );
    }
}