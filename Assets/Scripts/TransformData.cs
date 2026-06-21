using UnityEngine;

[CreateAssetMenu(
    fileName = "NewTransformData",
    menuName = "Save System/Transform Data"
)]
public class TransformData : ScriptableObject
{
    [Header("Player Position")]
    [SerializeField]
    private Vector2 position;

    [Header("Checkpoint Status")]
    [SerializeField]
    private bool hasSavedPosition;

    // Save Position
    public void SetPosition(
        Vector2 newPosition
    )
    {
        // Validasi data aneh
        if (float.IsNaN(newPosition.x) ||
            float.IsNaN(newPosition.y))
        {
            Debug.LogError(
                "Invalid Position Data!"
            );

            return;
        }

        position = newPosition;

        hasSavedPosition = true;
    }

    // Load Position
    public Vector2 GetPosition()
    {
        return position;
    }

    // Check apakah sudah pernah save
    public bool HasSavedPosition()
    {
        return hasSavedPosition;
    }

    // Reset Data
    public void ResetData()
    {
        position = Vector2.zero;

        hasSavedPosition = false;
    }
}