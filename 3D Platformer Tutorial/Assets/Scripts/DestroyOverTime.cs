using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy Automatically Once lifetime equals 0
        Destroy(gameObject, lifetime);
    }
}
