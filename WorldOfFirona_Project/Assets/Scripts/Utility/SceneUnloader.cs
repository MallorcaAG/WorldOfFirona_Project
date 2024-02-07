using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUnloader : MonoBehaviour
{
    [SerializeField] private string sceneToUnload;

    // This method can be called from a button click event in Unity UI
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
}
