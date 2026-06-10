using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionDistance = 5f;

    [SerializeField] private LayerMask detectionLayer;

    [Header("Enemy Movement")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform player;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player dengan tag 'Player' tidak ditemukan!");
        }
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (player == null)
            return;

        Vector2 direction =
            (player.position - transform.position).normalized;

        Debug.DrawRay(
            transform.position,
            direction * detectionDistance,
            Color.red
        );

        RaycastHit2D[] hits =
            Physics2D.RaycastAll(
                transform.position,
                direction,
                detectionDistance,
                detectionLayer
            );

        bool playerDetected = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
                continue;

            Debug.Log("Raycast Hit: " +
                      hit.collider.name);

            if (hit.collider.CompareTag("Ground"))
            {
                playerDetected = false;
                break;
            }

            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                break;
            }
        }

        if (playerDetected)
        {
            Debug.Log("Player Detected!");

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }
}