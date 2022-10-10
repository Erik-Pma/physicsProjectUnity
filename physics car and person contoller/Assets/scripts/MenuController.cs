using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int sceneI;
    public void QuitGame() 
    {
        Application.Quit();
    }

    public void switchScene() 
    {
        SceneManager.LoadScene(sceneI);
    }
    
}
