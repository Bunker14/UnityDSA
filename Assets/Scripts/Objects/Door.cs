using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public enum DoorType
{
    key,
    button
}

public class Door : Interectable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D physicsColliderPista;




    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("attack"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        physicsCollider.enabled = false;
        physicsColliderPista.enabled = false;
        open = true;
        

    }

    public void Close()
    {


    }

}
