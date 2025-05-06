using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private string nextScene;

//Function to load the game by switching to Scene 1 (SampleScene for now)
    public void PlayButton()
    {
        SceneManager.LoadScene(nextScene); 
    }

    //Function to quit game
    public void QuitButton()
    {
        Application.Quit();
    }
     
}
