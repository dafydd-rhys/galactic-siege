using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling2D : MonoBehaviour
{
    public float speed = 0.2f;

    [SerializeField]
    private Renderer backgroundRenderer;

    void Update()
{
    backgroundRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
}

}
