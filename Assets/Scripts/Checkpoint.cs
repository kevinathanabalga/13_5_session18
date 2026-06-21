using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerRespawn playerRespawn =
            other.GetComponent<PlayerRespawn>();

        PlayerPositionHandler positionHandler =
            other.GetComponent<PlayerPositionHandler>();

        if (playerRespawn == null)
        {
            Debug.LogWarning(
                "PlayerRespawn tidak ditemukan pada Player!"
            );
        }

        if (positionHandler == null)
        {
            Debug.LogWarning(
                "PlayerPositionHandler tidak ditemukan pada Player!"
            );
        }

        activated = true;

        if (playerRespawn != null)
        {
            playerRespawn.SetRespawnPoint(
                transform.position
            );
        }

        if (positionHandler != null)
        {
            positionHandler.SavePosition(
                transform.position
            );
        }

        ActivateCheckpointVisual();

        Debug.Log(
            $"Checkpoint Activated: {gameObject.name}"
        );
    }

    private void ActivateCheckpointVisual()
    {
        if (transform.childCount > 0)
        {
            transform
                .GetChild(0)
                .gameObject
                .SetActive(true);
        }
    }
}