using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    public void RestartGame() {
        SceneManager.LoadScene("LevelwithEnvironment");
    }
    public void MainMenuScreen() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops play mode in the editor
        #endif
        //Application.Quit();
    }
}
