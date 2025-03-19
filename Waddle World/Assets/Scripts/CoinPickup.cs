using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    public int value;

    public float spinSpeed;

    [SerializeField] GameObject pickupEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinSpin();
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

    private void CoinSpin()
    {
        transform.Rotate(0,spinSpeed,0);
    }
}
