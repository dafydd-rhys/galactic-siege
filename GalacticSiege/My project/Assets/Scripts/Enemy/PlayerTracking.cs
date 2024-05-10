using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public float enemyRotationSpeed = 90f;

    private Transform player;

    void Update()
    {
        if (player == null) {
            GameObject find = GameObject.FindWithTag("Player");

            if (find != null) {
                player = find.transform;
            }
        }

        if (player == null) {
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion expectedRotation = Quaternion.Euler(0, 0, z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, expectedRotation, enemyRotationSpeed * Time.deltaTime);
    }
}
