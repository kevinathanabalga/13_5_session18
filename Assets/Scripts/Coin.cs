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
        // Destroy coin otomatis setelah beberapa detik
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cegah collect dua kali
        if (isCollected)
            return;

        // Pastikan player yang mengambil coin
        if (collision.CompareTag("Player"))
        {
            isCollected = true;

            // Tambah score ke GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            else
            {
                Debug.LogError("GameManager Instance tidak ditemukan di scene!");
            }

            // Hancurkan coin
            Destroy(gameObject);
        }
    }
}