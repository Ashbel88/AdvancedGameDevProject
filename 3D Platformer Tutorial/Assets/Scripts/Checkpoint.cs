using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;

    public Material cpOn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theHealthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointOff()
    {
        theRend.material = cpOff;
    }
    public void CheckpointOn()
    {

        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        foreach(Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }

        theRend.material = cpOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            theHealthMan.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    }
}
