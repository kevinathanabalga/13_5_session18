using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float laserDistance = 10f;

    [SerializeField] private float laserDuration = 0.1f;

    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        // Auto get LineRenderer kalau belum di-assign
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    private void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;

            lineRenderer.positionCount = 2;
        }
        else
        {
            Debug.LogError("LineRenderer tidak ditemukan!");
        }
    }

    private void Update()
    {
        // Jangan bisa menembak saat pause
        if (GameManager.Instance != null &&
            GameManager.Instance.isPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootLaser();
        }
    }

    private void ShootLaser()
    {
        if (lineRenderer == null || firePoint == null)
        {
            Debug.LogError("LaserShooter reference missing!");

            return;
        }

        // Arah tembakan
        Vector2 direction = firePoint.right;

        // Debug ray di Scene View
        Debug.DrawRay(
            firePoint.position,
            direction * laserDistance,
            Color.red,
            1f
        );

        RaycastHit2D hit =
            Physics2D.Raycast(
                firePoint.position,
                direction,
                laserDistance,
                targetLayer
            );

        Vector3 endPosition;

        if (hit.collider != null)
        {
            endPosition = hit.point;

            Debug.Log(
                "Laser Hit: " +
                hit.collider.name
            );

            // Destroy enemy saja
            if (hit.collider.CompareTag("KillZone"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            endPosition =
                firePoint.position +
                (Vector3)direction * laserDistance;
        }

        StartCoroutine(
            ShowLaser(
                firePoint.position,
                endPosition
            )
        );
    }

    private IEnumerator ShowLaser(
        Vector3 start,
        Vector3 end)
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, start);

        lineRenderer.SetPosition(1, end);

        // realtime supaya tetap mati walau pause
        yield return new WaitForSecondsRealtime(
            laserDuration
        );

        lineRenderer.enabled = false;
    }
}