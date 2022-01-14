using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{


    public InventoryItem thisItem;
    public InventoryItem thisItem2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }


    public void EndGame()
    {
        if (thisItem.numberHeld == 1 && thisItem2.numberHeld == 1)
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}
