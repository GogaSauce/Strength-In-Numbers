using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void GoToGame()
    {
        
        SceneManager.LoadScene("SampleScene");
    }
}
