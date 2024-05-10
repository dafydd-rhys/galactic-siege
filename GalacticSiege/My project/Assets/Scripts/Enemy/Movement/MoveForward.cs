using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float movementSpeed = 5f;
    
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 velocity = new Vector3(0, movementSpeed * Time.deltaTime, 0);
        position += transform.rotation * velocity;
        transform.position = position;
    }
}
