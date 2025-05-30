using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    [SerializeField] GameObject pickupEffect;

    public int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerManager.AddCoin(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
