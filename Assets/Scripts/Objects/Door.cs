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
    public BoolValue storedOpen;
    public bool isOpen;
    //public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D physicsColliderPista;


    void Start()
    {
        
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            doorSprite.enabled = false;
            physicsCollider.enabled = false;
            physicsColliderPista.enabled = false;
        }
    }

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
        isOpen = true;
        

    }

    public void Close()
    {


    }

}
