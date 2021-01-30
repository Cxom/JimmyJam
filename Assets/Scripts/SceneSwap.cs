using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwap : MonoBehaviour
{
    /// <summary>
    /// Indicate the string of the next scene to load. Editable from the Unity UI
    /// So we can easilly change / edit scene transitions during development
    /// </summary>

    public string sceneName;
    public void NextScene()
    {
        Debug.Log("Swapping to Scene -> " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
