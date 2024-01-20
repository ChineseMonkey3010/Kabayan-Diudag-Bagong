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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that triggered the collider is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject); // Destroy the trap object when it hits the player
            UnityEngine.Debug.Log("Bangsad");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            UnityEngine.Debug.Log("Object collided with ground!");
        }
    }

    private void trapmovementupdate()
    {
        var movementObject = Time.deltaTime * speed;
        spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x - movementObject, spawnedObject.transform.position.y, spawnedObject.transform.position.z);

    }
}
