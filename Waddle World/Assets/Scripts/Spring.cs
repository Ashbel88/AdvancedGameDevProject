using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator springAnim;
    public int springForce;
    private bool isBounced;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBounced = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            isBounced = true;
            Vector3 direction = other.transform.position - transform.position;
            playerController.SpringLaunch(direction, springForce);

            StartCoroutine(ResetBounce());
        }
    }

    private void Animations()
    {
        springAnim.SetBool("isBounced", isBounced);
    }

    // Waits a bit before Resetting the Bounce

    private IEnumerator ResetBounce()
    {
        yield return new WaitForSeconds(2f);
        isBounced = false;
    }
}
