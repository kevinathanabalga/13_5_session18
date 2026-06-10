using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = GameObject.Find("RespawnPoint").transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            Respawn();
        }
    }

    public void SetRespawnPoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
