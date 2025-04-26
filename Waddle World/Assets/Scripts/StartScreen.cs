using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

//Function to load the game by switching to Scene 1 (SampleScene for now)
    public void PlayButton()
    {
        SceneManager.LoadScene(1); 
    }

    //Function to quit game
    public void QuitButton()
    {
        Application.Quit();
    }
     
}
