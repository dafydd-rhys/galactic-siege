using UnityEngine;

public class MultiBulletShooting : MonoBehaviour
{
    public float xGap = 0.15f;
    public float initialXOffset = -0.35f;
    public Bullet prefab;
    public float delay = 3f;
    public float cooldown = 0;
    private Camera mainCamera;
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
    for (int i = 0; i < 5; i++) {
        float x = initialXOffset + i * xGap;
        
        float y;
        if (i <= 2)
            y = 0.1f + i * 0.3f;
        else
            y = 0.7f - (i - 2) * 0.3f;
        
        Vector3 offset = new Vector3(x, y, 0);
        
        offset = transform.TransformDirection(offset);
        Bullet bullet = Instantiate(prefab, transform.position + offset, transform.rotation);
        bullet.Project(transform.up); 
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
