using UnityEngine;

public class CoinFish : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject fish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fish.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager.currentCoins >= 100)
        {
            fish.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
