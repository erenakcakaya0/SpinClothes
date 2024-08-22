using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        //Bir sonraki level
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void RestartScene()
    {
        //Aynı level'i tekarlatmak
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
