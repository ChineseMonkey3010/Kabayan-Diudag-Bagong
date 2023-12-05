using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject backgroundPrefab; // Reference to the background prefab

    private Transform player; // Reference to the player's transform
    private float backgroundWidth; // Width of the background prefab

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has a "Player" tag
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x; // Get the width of the background prefab
    }

    void Update()
    {
        // Check if the player has reached the end of the current background
        if (player.position.x >= transform.position.x + backgroundWidth / 2)
        {
            GenerateNewBackground(); // Generate a new background
        }
    }

    void GenerateNewBackground()
    {
        Vector3 newPosition = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
        Instantiate(backgroundPrefab, newPosition, Quaternion.identity); // Instantiate a new background
    }
}
