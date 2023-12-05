using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public bool isPlayer1; // Flag to identify if this is Player 1 or Player 2

    private Animator animator; // Reference to the Animator component
    private bool isRunning = true; // Flag to track if the player is running or not
    private bool canUseGacha = true; // Flag to track if the player can use the gacha system

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component

        // Start the running animation at the beginning of the game
        PlayRunningAnimation();
    }

    void Update()
    {
        if (isRunning)
        {
            if (isPlayer1 && Input.GetKeyDown(KeyCode.W))
            {
                StopRunningAndJump();
            }
            else if (!isPlayer1 && Input.GetKeyDown(KeyCode.I))
            {
                StopRunningAndJump();
            }
        }

        if (isPlayer1 && Input.GetKeyDown(KeyCode.X) && canUseGacha)
        {
            // Activate gacha system for Player 1
            ActivateGachaSystem();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.N) && canUseGacha)
        {
            // Activate gacha system for Player 2
            ActivateGachaSystem();
        }

        // Check for game-ending conditions (e.g., health reaches 0, boar catches player)
        // HandleGameEndingConditions();
    }

    void PlayRunningAnimation()
    {
        animator.SetBool("IsRunning", true); // Trigger the running animation
        isRunning = true;
    }

    void StopRunningAndJump()
    {
        animator.SetBool("IsRunning", false); // Stop the running animation
        animator.SetTrigger("Jump"); // Trigger the jump animation
        isRunning = false;
        // Logic to handle jumping action
        // After jumping action completes, call PlayRunningAnimation() to resume running
    }

    void ActivateGachaSystem()
    {
        // Logic to activate the gacha system for the respective player
        // Implement the gacha system functionality
        // Handle slot choices and gacha cooldown
        canUseGacha = false; // Disable gacha until cooldown expires
        // Reset gacha after a certain time using trapCooldown from PlayerMovement script
        // Set canUseGacha = true after cooldown time
    }

    // Other methods to handle game-ending conditions and winner announcement
    // void HandleGameEndingConditions() { ... }
    // void AnnounceWinner() { ... }
}
