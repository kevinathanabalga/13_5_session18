using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;

    [Header("Lifecycle Settings")]
    [SerializeField] private float lifeTime = 10f;

    private bool isCollected = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected)
            return;

        if (collision.CompareTag("Player"))
        {
            isCollected = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            else
            {
                Debug.LogError("GameManager Instance tidak ditemukan di scene!");
            }

            Destroy(gameObject);
        }
    }
}