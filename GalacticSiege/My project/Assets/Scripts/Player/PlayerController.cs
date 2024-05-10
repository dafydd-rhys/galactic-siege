using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    [SerializeField] private float thrustingSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private HealthBar hBar;
    [SerializeField] private HealthBar sBar;
    [SerializeField] private int boosts = 3;
    [SerializeField] private int maxBoosts = 3;
    [SerializeField] private float boostForce = 1f; 
    [SerializeField] private float boostCooldown = 0f; 
    [SerializeField] private float boostDelay = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private AudioClip audioShoot;
    [SerializeField] private AudioClip audioBoost;

    private float cooldown = 0;
    private Rigidbody2D body;
    private bool thrusting;
    private float rotation;
    private bool shooting;
    private bool boosting;
    private float originalDelay;

    private bool canThrust = true;
    private bool canRotate = true;
    private bool canBoost = true;
    private bool canShoot = true;

    public void SetPlayerBools(bool t, bool r, bool s, bool b) {
        canThrust = t;
        canRotate = r;
        canShoot = s;
        canBoost = b;
    }

    private void Start() {
        MaxHealth();
        MaxBoosts();
        originalDelay = delay;
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        boostCooldown -= Time.deltaTime;

        thrusting = Input.GetAxis("Vertical") > 0;
        rotation = Input.GetAxis("Horizontal");
        shooting = Input.GetAxis("Shoot") > 0;
        boosting = Input.GetAxis("Boost") > 0;
        
        if (shooting && cooldown <= 0  && canShoot) {
            cooldown = delay;
            Shoot();
        }

        if (boosting && boosts > 0 && boostCooldown <=0  && canBoost) {
            boostCooldown = boostDelay;
            Boost();
        }

        if (health <= 0) {
            Die();
        }
    }

    private void Boost()
    {
        AudioServiceLocator.PlaySFX(audioBoost);
        Vector2 boostDirection = transform.up;
        body.AddForce(boostDirection * boostForce, ForceMode2D.Impulse);
        boosts--;
        sBar.SetValue(boosts);
    }

    private void FixedUpdate()
    {
        if (thrusting && canThrust) {
            body.AddForce(transform.up * thrustingSpeed);
        }

        if (rotation != 0.0f && canRotate){
            body.AddTorque(-rotation * rotationSpeed);
        }
    }

    private void Shoot() {
        AudioServiceLocator.PlaySFX(audioShoot);
        Vector3 offset = transform.rotation * bulletOffset;
        Bullet bullet = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnTriggerEnter2D() {
        TakeDamage(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject eObject = collision.gameObject;

        if (eObject.tag == "Asteroid") {
            Asteroid asteroid = eObject.GetComponent<Asteroid>();   

            if (asteroid.size >= 0.3f) {
                TakeDamage(50);
            } else {
                TakeDamage(33);
            }    
        } else if (eObject.tag == "EnemyBullet") {
            TakeDamage(50);
        } else if (eObject.tag == "Missile") {
            TakeDamage(200);
        } else if (eObject.tag == "Enemy") {
            TakeDamage(100);
        } 

        body.velocity = Vector3.zero;
        body.angularVelocity = 0.0f;
    }

    private void Die() {
        FindObjectOfType<GameManager>().PlayerDied();
    }

    private void TakeDamage(float damage) {
        health -= damage;
        hBar.SetValue(health);
    }

    public void Reset() {
        health = maxHealth;
        boosts = maxBoosts;
        hBar.SetMax(maxHealth);
        sBar.SetMax(maxBoosts);
    }


    public void HandleLaserHit()
    {
        TakeDamage(0.1f);
    }

     public void ResetDelay() {
        delay = originalDelay;
    }

    public Rigidbody2D GetBody() {
        return body;
    }

    public void MaxBoosts() {
        boosts = maxBoosts;
        sBar.SetValue(maxBoosts);
    }

    public void MaxHealth() {
        health = maxHealth;
        hBar.SetValue(maxHealth);
    }

    public void SetDelay(float newDelay) {
        delay = newDelay;
    }

    public float SetDelay() {
        return delay;
    }

    public HealthBar GetHBar() {
        return hBar;
    }

    public HealthBar GetSBar() {
        return sBar;
    }

    public TextMeshProUGUI GetScore() {
        return score;
    }

    public void SetSpriteRenderer(SpriteRenderer newSpriteRenderer) {
        spriteRenderer = newSpriteRenderer;
    }

    public SpriteRenderer GetSpriteRenderer() {
        return spriteRenderer;
    }

    public void SetHealth(float newHealth)
    {
        health = Mathf.Clamp(newHealth, 0f, maxHealth);
        hBar.SetValue(health);
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetBoosts(int newBoosts)
    {
        boosts = Mathf.Clamp(newBoosts, 0, maxBoosts);
        sBar.SetValue(boosts);
    }

    public int GetBoosts()
    {
        return boosts;
    }

        public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = Mathf.Max(newMaxHealth, 0f);
        hBar.SetMax(maxHealth);

        health = Mathf.Min(health, maxHealth);
        hBar.SetValue(health);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxBoosts(int newMaxBoosts)
    {
        maxBoosts = Mathf.Max(newMaxBoosts, 0); 
        sBar.SetMax(maxBoosts);

        boosts = Mathf.Min(boosts, maxBoosts);
        sBar.SetValue(boosts);
    }

    public int GetMaxBoosts()
    {
        return maxBoosts;
    }

}
