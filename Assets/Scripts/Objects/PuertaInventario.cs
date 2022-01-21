using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInventario : MonoBehaviour
{

    public InventoryItem thisItem;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbrirPuerta();
        }
    }

    public void AbrirPuerta()
    {
        if (thisItem.numberHeld >= 1)
        {
            doorSprite.enabled = false;
            physicsCollider.enabled = false;
        }
    }

}
