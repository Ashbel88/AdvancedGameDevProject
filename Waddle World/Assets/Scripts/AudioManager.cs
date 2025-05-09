using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXsource;

    public AudioClip background; 
    public AudioClip background2; 
    public AudioClip coin; 
    public AudioClip health;
    public AudioClip jump;
    public AudioClip damage; 
    public AudioClip win;
    public AudioClip death;

    public GameObject gameOver;
    public GameObject winScreen; 
    private void Start(){
        musicSource.clip= background;
        musicSource.Play();



    }

    private void Update(){

        if (gameOver.activeInHierarchy || winScreen.activeInHierarchy){
            if (musicSource.isPlaying){
            musicSource.Pause();
            }
        } else{
            if (!musicSource.isPlaying){
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip){
        SFXsource.PlayOneShot(clip);
    }

}
