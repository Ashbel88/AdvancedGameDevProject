using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public PlayerManager playerScript;
    //for the buttons on win screen

    public void ResumeButton(){
        Time.timeScale = 1f;
        playerScript.hasWon = false;
        playerScript.winScreenUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CameraController>().enabled = true;

    }

    public void QuitButton(){
        SceneManager.LoadScene(0);
    }
}
