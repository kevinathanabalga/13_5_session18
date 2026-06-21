using UnityEngine;

public class PlayerPositionHandler : MonoBehaviour
{
    [Header("Player Position")]
    [SerializeField] private Vector2 playerCurrentPosition;

    [SerializeField] private Vector2 currentCheckpointPosition;

    [Header("Save System")]
    [SerializeField] private TransformData playerPositionData;

    private void Start()
    {
        if (playerPositionData == null)
        {
            Debug.LogError(
                "TransformData belum diassign pada PlayerPositionHandler!"
            );

            return;
        }

        LoadPosition();
    }

    #region Checkpoint System

    public void OnCheckpoint(GameObject checkpoint)
    {
        Vector2 newCheckpointPosition =
            checkpoint.transform.position;

        currentCheckpointPosition =
            newCheckpointPosition;

        SavePosition(
            currentCheckpointPosition
        );

        CheckpointWallActive(
            checkpoint
        );

        Debug.Log(
            "Checkpoint Saved"
        );
    }

    private void CheckpointWallActive(
        GameObject checkpoint)
    {
        if (checkpoint.transform.childCount > 0)
        {
            checkpoint.transform
                .GetChild(0)
                .gameObject
                .SetActive(true);
        }
    }

    #endregion

    #region Trap / Respawn System

    public void OnTrap()
    {
        LoadPosition();

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity =
                Vector2.zero;
        }

        Debug.Log(
            "Player Respawned"
        );
    }

    public void OnFinish()
    {
        playerPositionData.ResetData();

        currentCheckpointPosition =
            Vector2.zero;

        playerCurrentPosition =
            Vector2.zero;

        Debug.Log(
            "Position Data Reset"
        );
    }

    #endregion

    #region Save & Load Position

    public void SavePosition(
        Vector2 newPosition)
    {
        currentCheckpointPosition =
            newPosition;

        playerPositionData.SetPosition(
            newPosition
        );

        Debug.Log(
            "Position Saved: "
            + newPosition
        );
    }

    public void LoadPosition()
    {
        if (playerPositionData == null)
        {
            return;
        }

        playerCurrentPosition =
            playerPositionData.GetPosition();

        // Jangan load jika belum pernah checkpoint
        if (playerCurrentPosition ==
            Vector2.zero)
        {
            return;
        }

        ChangePlayerPosition(
            playerCurrentPosition
        );

        Debug.Log(
            "Position Loaded: "
            + playerCurrentPosition
        );
    }

    private void ChangePlayerPosition(
        Vector2 newPosition)
    {
        transform.position =
            newPosition;
    }

    #endregion
}