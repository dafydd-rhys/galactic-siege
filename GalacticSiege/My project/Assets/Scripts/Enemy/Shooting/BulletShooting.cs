using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    private Camera mainCamera;
    public Vector3 bulletOffset = new Vector3(0, 0.53f, 0);
    public Bullet prefab;
    public float delay = 0.5f;
    public float cooldown = 0;
    private bool within = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (within == false) {
            within = WithinBoundaries();
        } else {
            cooldown -= Time.deltaTime;
        }
        
        if (cooldown <= 0 && within) {
            cooldown = delay;
            Shoot();
        }
    }

    private void Shoot() {
        Vector3 offset = transform.rotation * bulletOffset;
        Bullet bullet = Instantiate(prefab, transform.position + offset, transform.rotation);
        bullet.Project(transform.up);
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
