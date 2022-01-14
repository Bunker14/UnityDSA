using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PostFinal : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
