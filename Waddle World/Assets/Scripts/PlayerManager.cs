using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    private int currentCoins = 0;
    public int maxHealth;
    private int currentHealth;

   [SerializeField] private TextMeshProUGUI coinText;
   [SerializeField] private TextMeshProUGUI healthText;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;

    // Character Model Flashing Taking Damage (Will make more sense when we have a Character Model)
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    //For game over + win screen
    public GameObject gameOverUI;
    public bool isDead;
    public GameObject winScreenUI;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "" + currentHealth;
        coinText.text = "" + currentCoins;

    }

    // Update is called once per frame
    void Update()
    {
        Invicibility();
        gameOver();
    }

    public void AddCoin(int coinToAdd)
    {
        currentCoins += coinToAdd;
        coinText.text = "" + currentCoins;
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
       if(invincibilityCounter <=0)
        {
            thePlayer.anim.SetTrigger("isHurt");
            currentHealth -= damage;
            healthText.text = "" + currentHealth;

            if(currentHealth <= 0)
            {
                Respawn();
            } 
            else
            {
                thePlayer.Knockback(direction);

                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;

               flashCounter = flashLength;
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
         currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Invicibility()
    {
         if(invincibilityCounter >0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if(flashCounter <= 0)
            {
                // Character Model Flashing

               playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }

        }
    }

    public void Respawn()
    {
        if(!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;

        
        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;
        healthText.text = "" + currentHealth;

        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;
    }

     public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;

        // print(respawnPoint); for testing (:
    }

    //When player health reaches 0, goes to game over screen
        public void gameOver(){

        if (currentHealth <= 0 && !isDead){
            isDead = true;
            gameOverUI.SetActive(true);

            if (gameOverUI.activeInHierarchy){
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Camera.main.GetComponent<CameraController>().enabled = false;
            } else{
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }


}

