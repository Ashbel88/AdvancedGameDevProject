using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


     //Allows user to click on a button to restart level
    public void retryButton(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Allows user to quit and go to start screen
    public void quitButton(){
        SceneManager.LoadScene(0);
    }

}
