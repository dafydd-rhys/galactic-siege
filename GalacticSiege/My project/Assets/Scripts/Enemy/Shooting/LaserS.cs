using UnityEngine;

public class LaserS : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform laserPosition;
    public float delay = 15f;
    public float cooldown = 3f;
    public float duration = 7.5f;
    private bool shooting = false;
    public float yOffset = 0.45f;
    private Camera mainCamera;
    private bool within = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start() {
        lineRenderer.sortingOrder = -1;
    }

    private void Update()
    {
        if (within == false) {
            within = WithinBoundaries();
        } else {
            cooldown -= Time.deltaTime;
        }
        
        if (cooldown <= 0 && !shooting && within)
        {
            cooldown = delay;
            shooting = true;
            duration = 7.5f;
            ShootLaser();
        }

        if (shooting)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                shooting = false;
                lineRenderer.SetPosition(1, lineRenderer.GetPosition(0));
            }
            else
            {
                ShootLaser();
            }
        }
    }

private void ShootLaser()
{
    Vector3 startPosition = laserPosition.position + transform.up * yOffset;
    RaycastHit2D hit = Physics2D.Raycast(startPosition, transform.up);

    lineRenderer.SetPosition(0, startPosition);
    
    if (hit)
    {   
        if (hit.collider.CompareTag("Player")) {
            PlayerController player = hit.collider.GetComponent<PlayerController>();
            if (player != null) {
                player.HandleLaserHit();
            }
        }

        lineRenderer.SetPosition(1, hit.point);
    }
    else
    {
        lineRenderer.SetPosition(1, startPosition + transform.up * 25);
    }
}

private bool WithinBoundaries()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            return false;
        } else {
            return true;
        }
    }

}
