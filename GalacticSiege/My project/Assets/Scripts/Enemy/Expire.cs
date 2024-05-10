using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expire : MonoBehaviour
{
    public float delay = 10;

    void Start()
    {
        Destroy(gameObject, delay);
    }

}
