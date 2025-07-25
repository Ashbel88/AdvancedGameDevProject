using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string nextScene;

     //Allows user to click on a button to restart level
    public void RetryButton(){
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(nextScene);
        
    }

    //Allows user to quit and go to start screen
    public void QuitButton(){
        Application.Quit();
    }

}
