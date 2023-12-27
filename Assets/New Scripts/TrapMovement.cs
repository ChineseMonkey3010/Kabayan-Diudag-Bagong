using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    public GameObject spawnedObject;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trapmovementupdate();
        checktrapposition();
        
    }

    private void checktrapposition()
    {
        if (spawnedObject.transform.position.x <= -10)
        {
            Destroy(spawnedObject);
        }
    }

    private void trapmovementupdate()
    {
        var movementObject = Time.deltaTime * speed;
        spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x - movementObject, transform.position.y, transform.position.z);
    }
}
