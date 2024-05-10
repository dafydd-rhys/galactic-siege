using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 0.2f;
    public float speed = 5.0f;
    public float lifetime = 15.0f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    public Abilities Ability { get; private set; }

    public enum Abilities
    {
        Boost,
        Health,
        Invincibility,
        Nuclear,
        TimeSlow,
        Laser,
        x2,
        DeathMachine
    }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Abilities[] abilities = {
            Abilities.Boost,
            Abilities.Health,
            Abilities.Invincibility,
            Abilities.Nuclear,
            Abilities.TimeSlow,
            Abilities.Laser,
            Abilities.x2,
            Abilities.DeathMachine
        };

         int random = -1;
        if (random == -1) {
            random = Random.Range(0, sprites.Length);
        }
        spriteRenderer.sprite = sprites[random];
        Ability = abilities[random];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;
        rigidBody.mass = size * 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.PowerUpDestroyed(this);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }

    public void SetTrajectory(Vector2 direction) {
        rigidBody.AddForce(direction * speed);

        Destroy(gameObject, lifetime);
    }

    public void ActivatePowerUp(PlayerController player, GameManager gameManager)
    {
        PowerUps powerUpsInstance = PowerUpFactory.CreatePowerUp(Ability);

        if (powerUpsInstance != null)
        {
            powerUpsInstance.ApplyPowerUp(player, gameManager);
        }
        else
        {
            Debug.LogError("Failed to create power-up instance for ability: " + Ability);
        }
    }


}