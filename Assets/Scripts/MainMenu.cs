using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("LevelwithEnvironment");
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops play mode in the editor
        #endif        
        //Application.Quit();
    }
}
