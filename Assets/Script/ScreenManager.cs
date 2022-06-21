using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    
    public void OnStart()
    {
        SceneManager.LoadScene(0);
    }


    public void OnGameScreen()
    {
        SceneManager.LoadScene(1);
    }

   
}
