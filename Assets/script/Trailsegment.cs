using System;
using UnityEngine;

public class Trailsegment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Eraser"))
        {
            Destroy(gameObject);
        }
    }

}
