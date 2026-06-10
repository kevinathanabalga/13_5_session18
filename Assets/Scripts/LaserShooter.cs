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

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootLaser();
        }
    }

    private void ShootLaser()
    {
        Vector2 direction = transform.right;

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

            Debug.Log("Laser Hit: "
                      + hit.collider.name);

            Destroy(hit.collider.gameObject);
        }
        else
        {
            endPosition =
                firePoint.position +
                (Vector3)direction *
                laserDistance;
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

        yield return new WaitForSeconds(
            laserDuration);

        lineRenderer.enabled = false;
    }
}