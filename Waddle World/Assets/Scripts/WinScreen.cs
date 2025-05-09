using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public PlayerManager playerScript;
    //for the buttons on win screen

    public void resumeButton(){
        playerScript.winScreenUI.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CameraController>().enabled = true;

    }

    public void quitButton(){
        SceneManager.LoadScene(0);
    }
}
