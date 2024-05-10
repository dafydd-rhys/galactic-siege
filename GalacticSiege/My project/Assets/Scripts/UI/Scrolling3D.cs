using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to include RawImage

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float x, y;

    void Update() {
        Rect uvRect = img.uvRect;
        uvRect.position += new Vector2(x, y) * Time.deltaTime;
        uvRect.position = new Vector2(
            Mathf.Repeat(uvRect.position.x, 1f),
            Mathf.Repeat(uvRect.position.y, 1f)
        );
        img.uvRect = uvRect;
    }
}
