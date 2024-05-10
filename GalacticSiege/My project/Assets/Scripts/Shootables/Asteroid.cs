using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 0.2f;
    public float minSize = 0.15f;
    public float maxSize = 0.4f;
    public float speed = 5.0f;
    public float lifetime = 15.0f;
    public int reward = 50;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private int random = -1;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (random == -1) {
            random = Random.Range(0, sprites.Length);
        }
        spriteRenderer.sprite = sprites[random];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;
        rigidBody.mass = size * 2;
    }

    public void SetTrajectory(Vector2 direction) {
        rigidBody.AddForce(direction * speed);

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet") {
            if ((size * 0.5f) >= minSize)  {
                if (random != -1) {
                    CreateSplit();
                    CreateSplit();
                }
            }
        } 

        FindObjectOfType<GameManager>().AsteroidDestroyed(this); 
        Destroy(gameObject);
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid halve = Instantiate(this, position, transform.rotation);
        halve.random = random;
        halve.size = size * 0.5f;
        halve.SetTrajectory(Random.insideUnitCircle.normalized * (2 * speed));
        Destroy(halve, lifetime);
    }
}
