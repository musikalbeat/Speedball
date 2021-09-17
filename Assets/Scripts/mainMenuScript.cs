using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void exitGame(){
        Application.Quit();
    }
}
