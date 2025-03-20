using UnityEngine;

public class Spring : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    public int springForce;

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
            Vector3 direction = other.transform.position - transform.position;
            playerController.SpringLaunch(direction, springForce);
        }
    }
}
