using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    // Call this function to quit the game
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // If running in the Unity editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
