using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    // This method can be called from a button click event in Unity UI
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneAdditive()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }
}
