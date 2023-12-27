using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public bool isPlayer1; // Flag to identify if this is Player 1 or Player 2
    private bool player1CanGacha = true;
    private bool player2CanGacha = true;
    private float gachaCooldown = 30f;
    public float initialPositionObject;
    private Rigidbody2D rb;
    public bool Grounded;
    public string groundTag = "Ground"; // Reference to an empty GameObject placed at the player's feet
    public LayerMask groundLayer;
    private bool canJump = true;
    public float jumpDuration = 1f;

    public GameObject spawnedObject;
    public Animator playerAnimator;
    private float speed = 1f; 
    public float jumpForce = 5.0f; // Reference to the Animator component
    private bool isRunning = true;
    private bool isJumping = false; // Flag to track if the player is running or not
    private bool canUseGacha = true;
    public float playerHealth = 100f;

    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    public TrapMovement trapMovement; // Flag to track if the player can use the gacha system

    private bool isPoisoned = false;
    private bool isWithering = false;
    private bool isConstantPoison = false;
    private bool isAlive = true;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Get the Animator component
        rb = GetComponent<Rigidbody2D>();
        // Start the running animation at the beginning of the game
        PlayRunningAnimation();
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f, groundLayer);
        Grounded = colliders.Any(collider => collider.CompareTag(groundTag));
        //UnityEngine.Debug.Log("Number of colliders detected: " + colliders.Length);
        if (isRunning)
        {
            if (Grounded)
            {
                if (isPlayer1 && Input.GetKeyDown(KeyCode.W))
                {
                    StopRunningAndJump();
                    PlayRunningAnimation();
                }
                else if (!isPlayer1 && Input.GetKeyDown(KeyCode.I))
                {
                    StopRunningAndJump();
                    PlayRunningAnimation();
                }
            }
        }

        //if (isPlayer1 && Input.GetKeyDown(KeyCode.X) && canUseGacha)
        //{
        //    // Activate gacha system for Player 1
        //    //ActivateGachaSystem(1);
        //    UnityEngine.Debug.Log("Gacha Activated");
        //}
        //else if (!isPlayer1 && Input.GetKeyDown(KeyCode.N) && canUseGacha)
        //{
        //    // Activate gacha system for Player 2
        //    //ActivateGachaSystem(2);
        //    UnityEngine.Debug.Log("Gacha Activated");
        //}

        if (Grounded)
        {
            playerAnimator.SetBool("isJumping", false); // Set the IsJumping parameter to false when grounded
        }

        // Check for game-ending conditions (e.g., health reaches 0, boar catches player)
        // HandleGameEndingConditions();
    }

    void PlayRunningAnimation()
    {
        playerAnimator.SetBool("isRunning", true);
        playerAnimator.Play("PlayerRunning"); // Trigger the running animation
        isRunning = true;
    }

    void StopRunningAndJump()
    {
        playerAnimator.SetBool("isRunning", false); // Stop the running animation
        playerAnimator.SetTrigger("isJumping");
        Grounded = false;
        isJumping = true;
        playerAnimator.Play("PlayerJump"); // Assuming "JumpAnimation" is the jumping animation's name

        // Apply jump force to move the player upwards
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isRunning = false;
        isJumping = false;

        Invoke("PlayRunningAnimation", jumpDuration); // Revert to running after jumpDuration time
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Check if the player collides with the ground object tagged as "Ground"
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        // Transition to running animation when landing on the ground
    //        playerAnimator.SetBool("IsJumping", false); // Set IsJumping parameter to false
    //    }
    //}

    //public void ActivateGachaSystem(int playerNumber)
    //{
    //    int trap = Random.Range(1, 6); // Generates numbers from 1 to 5 for traps
    //    int spell = Random.Range(1, 8); // Generates numbers from 1 to 7 for spells
    //    int effect = Random.Range(1, 5); // Generates numbers from 1 to 4 for effects

    //    UnityEngine.Debug.Log("Player " + playerNumber + " Gacha Results:");
    //    UnityEngine.Debug.Log("Trap: " + GetTrapName(trap));
    //    UnityEngine.Debug.Log("Spell: " + GetSpellName(spell));
    //    UnityEngine.Debug.Log("Effect: " + GetEffectName(effect));
    //    canUseGacha = false; // Disable gacha until cooldown expires
    //    // Reset gacha after a certain time using trapCooldown from PlayerMovement script
    //    // Set canUseGacha = true after cooldown time
    //}

    //private string GetTrapName(int trapNumber)
    //{
    //    switch (trapNumber)
    //    {
    //        case 1: return "Spiketrap";
    //        case 2: return "Landmine";
    //        case 3: return "Icespiketrap";
    //        case 4: return "Mud";
    //        case 5: return "Cobweb";
    //        default: return "Invalid Trap";
    //    }
    //}

    //private string GetSpellName(int spellNumber)
    //{
    //    switch (spellNumber)
    //    {
    //        case 1: return "NONE";
    //        case 2: return "Poison";
    //        case 3: return "Withering";
    //        case 4: return "Constant Poison";
    //        case 5: return "Harmness";
    //        case 6: return "Harmness Ultimate";
    //        case 7: return "Instant Death";
    //        default: return "Invalid Spell";
    //    }
    //}

    //private string GetEffectName(int effectNumber)
    //{
    //    switch (effectNumber)
    //    {
    //        case 1: return "NONE";
    //        case 2: return "Healing";
    //        case 3: return "Advanced Healing";
    //        case 4: return "Nullifier";
    //        default: return "Invalid Effect";
    //    }
    //}

    ////Traps
    //void Spiketrap()
    //{
    //    Vector3 position = new Vector3(initialPositionObject, transform.position.y, transform.position.z);
    //    spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);

    //}

    //void Landmine()
    //{
    //    Vector3 position = new Vector3(initialPositionObject, transform.position.y, transform.position.z);
    //    spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
    //}

    //void Icespiketrap()
    //{
    //    Vector3 position = new Vector3(initialPositionObject, transform.position.y, transform.position.z);
    //    spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
    //}

    //void Mud()
    //{
    //    Vector3 position = new Vector3(initialPositionObject, transform.position.y, transform.position.z);
    //    spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
    //}
    //void Cobweb()
    //{
    //    Vector3 position = new Vector3(initialPositionObject, transform.position.y, transform.position.z);
    //    spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
    //}

    ////Spells
    //void Poison()
    //{
    //    if (!isPoisoned)
    //    {
    //        isPoisoned = true;
    //        StartCoroutine(InflictPoisonDamage()); 
    //    }
    //}

    //IEnumerator InflictPoisonDamage()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        yield return new WaitForSeconds(1.0f);
    //        playerHealth--;
    //        UnityEngine.Debug.Log("Player Health: " + playerHealth); 
    //    }

    //    isPoisoned = false; 
    //}

    //void Withering()
    //{

    //}

    //void ConstantPoison()
    //{

    //}

    //void Harmness()
    //{

    //}

    //void HarmnessUltimate()
    //{

    //}

    //void InstantDeath()
    //{

    //}

    ////Effects
    //void Healing()
    //{

    //}

    //void AdvancedHealing()
    //{

    //}

    //void Nullifier()
    //{

    //}
    // Other methods to handle game-ending conditions and winner announcement
    // void HandleGameEndingConditions() { ... }
    // void AnnounceWinner() { ... }
}
