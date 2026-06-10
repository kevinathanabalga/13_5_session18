using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;

    [Header("Spawner Options")]
    [SerializeField] private bool onlySpawnIfNoCoinExists = true;

    private GameObject currentCoin;

    private void Start()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("Coin Prefab belum di-assign pada CoinSpawner!");
            return;
        }

        SpawnCoin();

        InvokeRepeating(
            nameof(TrySpawnCoin),
            spawnInterval,
            spawnInterval
        );
    }

    private void TrySpawnCoin()
    {
        if (onlySpawnIfNoCoinExists)
        {
            if (currentCoin == null)
            {
                SpawnCoin();
            }
        }
        else
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        currentCoin = Instantiate(
            coinPrefab,
            transform.position,
            Quaternion.identity
        );
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}