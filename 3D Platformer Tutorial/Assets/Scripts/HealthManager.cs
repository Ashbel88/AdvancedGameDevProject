using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter >0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if(invincibilityCounter <=0)
        {
            thePlayer.Knockback(direction);
            currentHealth -= damage;

            invincibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;

            flashCounter = flashLength;
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
}
