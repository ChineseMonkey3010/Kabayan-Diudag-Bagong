using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 5.0f;
    public float trapCooldown = 5.0f; // Cooldown time for gacha/trap generation
    public GameObject trapPrefab; // Reference to the trap prefab
    public Transform trapSpawnPoint; // Point to spawn the trap

    private Rigidbody2D rb;
    private float trapTimer = 0.0f;
    private bool canGenerateTrap = true;
    private int health = 100; // Player's health
    public HealthBar healthBar; // Reference to the HealthBar script for displaying health

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health); // Set the maximum health in the HealthBar script
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxis("Horizontal"); // Assuming horizontal movement
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);

        // Trap generation
        if (canGenerateTrap && Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTrap();
            canGenerateTrap = false;
            trapTimer = trapCooldown;
        }

        // Trap cooldown
        if (!canGenerateTrap)
        {
            trapTimer -= Time.deltaTime;
            if (trapTimer <= 0)
            {
                canGenerateTrap = true;
            }
        }
    }

    void GenerateTrap()
    {
        Instantiate(trapPrefab, trapSpawnPoint.position, Quaternion.identity); // Spawn trap at the spawn point
    }

    // Deduct health if player is caught by a trap or the boar
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health); // Update health bar
        if (health <= 0)
        {
            // Player defeated
            Debug.Log("Player Defeated");
            // Perform defeat actions (e.g., game over, reset, etc.)
        }
    }
}

