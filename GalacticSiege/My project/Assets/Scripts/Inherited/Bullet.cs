using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed = 500.0f;
    public float halflife = 5.0f;
    private Camera mainCamera;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void Project(Vector2 direction)
    {
        body.AddForce(direction * speed);
        Destroy(gameObject, halflife * 2);
    }

    void Update()
    {
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        if (viewPos.x < -0.2 || viewPos.x > 1.2 || viewPos.y < -0.2 || viewPos.y > 1.2)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }


}
