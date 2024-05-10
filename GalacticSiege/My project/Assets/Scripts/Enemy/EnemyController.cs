using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public HealthBar bar;
    public int reward = 100;
    private Rigidbody2D body;

    private void Start() {
        health = maxHealth;
        bar.SetMax(maxHealth);
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health <= 0) {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CollisionDetected(collision.gameObject);
    }

    private void CollisionDetected(GameObject eObject) {
         if (eObject.tag == "Asteroid") {
            Asteroid asteroid = eObject.GetComponent<Asteroid>();   

            if (asteroid.size >= 0.3f) {
                TakeDamage(50);
            } else {
                TakeDamage(33);
            }
        } else if (eObject.tag == "PlayerBullet") {
            TakeDamage(100);
        } else if (eObject.tag == "EnemyBullet") {
            TakeDamage(50);
        } else if (eObject.tag == "Player") {
            TakeDamage(200);
        }

        body.velocity = Vector3.zero;
        body.angularVelocity = 0.0f;
    }

    private void Die() {
        FindObjectOfType<GameManager>().EnemyDestroyed(this); 
        Destroy(gameObject);
    }

    private void TakeDamage(int damage) {
        health -= damage;
        bar.SetValue(health);
    }

}
