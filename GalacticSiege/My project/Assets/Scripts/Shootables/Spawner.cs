using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Asteroid aPrefab;
    public PowerUp puPrefab;
    public GameObject[] enemies;
    public float asteroidRate = 2.0f;
    public float asteroidDelay = 5.0f;
    public int asteroidAmount = 1;
    public float powerUpRate = 2.0f;
    public float powerUpDelay = 5.0f;
    public int powerUpAmount = 1;
    public float enemyRate = 2.0f;
    public float enemyDelay = 5.0f;
    public int enemyAmount = 1;
    public float distance = 20.0f;
    public float trajectoryVariance = 15.0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), asteroidDelay, asteroidRate);
        InvokeRepeating(nameof(SpawnPowerUp), powerUpDelay, powerUpRate);
        InvokeRepeating(nameof(SpawnEnemy), enemyDelay, enemyRate);
    }

    private void SpawnAsteroid()
    {
        for (int i = 0; i < asteroidAmount; i++) {    
            Vector3 direction = Random.insideUnitCircle.normalized * distance;
            Vector3 origin = transform.position + direction;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            if (aPrefab != null) {
                Asteroid asteroid = Instantiate(aPrefab, origin, rotation);
                asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
                asteroid.SetTrajectory(rotation * -direction);
            } 
        }
    }

     private void SpawnPowerUp()
    {
        for (int i = 0; i < powerUpAmount; i++) {    
            Vector3 direction = Random.insideUnitCircle.normalized * distance;
            Vector3 origin = transform.position + direction;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            if (puPrefab != null) {
                PowerUp powerUp = Instantiate(puPrefab, origin, rotation);
                powerUp.SetTrajectory(rotation * -direction);
            }   
        }
    }

     private void SpawnEnemy()
    {
        for (int i = 0; i < enemyAmount; i++) {    
            Vector3 direction = Random.insideUnitCircle.normalized * distance;
            Vector3 origin = transform.position + direction;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            int random = Random.Range(0, enemies.Length);
            GameObject newEnemy = Instantiate(enemies[random], origin, rotation);
            Rigidbody2D enemyRigidbody = newEnemy.GetComponent<Rigidbody2D>();
            MoveForward enemyMovement = newEnemy.GetComponent<MoveForward>();

            if (newEnemy != null && enemyRigidbody != null && enemyMovement != null) {
                enemyRigidbody.AddForce(rotation * -direction * enemyMovement.movementSpeed);
            }   
        }
     }
}