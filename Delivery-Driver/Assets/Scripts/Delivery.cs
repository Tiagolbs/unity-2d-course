using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1 , 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1 , 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage = false;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(collision.gameObject, destroyDelay);
        }

        if (collision.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
    }
}
