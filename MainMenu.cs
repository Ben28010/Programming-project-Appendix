using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene in the build index when the button is pressed
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false; //ends the program in the unity editor
        //Application.Quit(); useless when testing as it doesnt close program in inspector
    }
}
 