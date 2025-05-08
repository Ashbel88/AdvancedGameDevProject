using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    public int damageToGive;

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
        Vector3 hitDirection = other.transform.position - transform.position;
        hitDirection = hitDirection.normalized;
        playerManager.HurtPlayer(damageToGive, hitDirection);
    }
}
