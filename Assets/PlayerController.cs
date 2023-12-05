using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Character 
    public int maxHealth = 100;
    private int currentHealth;
    public float moveSpeed = 5.0f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    private Animator animator;
    public bool isGrounded;
    public bool isFacingRight = true;
    public float airControl = 0.2f;
    public bool canMove = true;
    public bool canJump = true;
    public float interactionRange = 10f;

    public float poisonDamagePerSecond = 5.0f;
    public bool isPoisoned = false;

    //Character
    private Rigidbody2D rb;
    private bool jump;
    private bool moveLeft;
    private bool moveRight;
    private bool jumpAndMoveLeft;
    private bool jumpAndMoveRight;
    private float horizontalMove;
    private float verticalMove;
    private bool isJumping = false;
    private float normalMoveSpeed;

    private bool isInvincible = false;
    private float invincibilityDuration = 5f;
    
    private bool isMeteorResistanceActive = false;
    private float meteorResistanceDuration = 5f;

    private bool isFakeMeteorResistanceActive = false; 

    private bool hasRevived = false;

    public float sprintSpeedMultiplier = 1.25f; // 25% increase in movement speed.
    private bool isSprinting = false;
    public float sprintMoveSpeed; 

    private bool isFakeSprintActive = false; // Flag to track if FakeSprint is active.
    private float originalMoveSpeed;

    private Vector3 initialPosition;

    private bool isFakeHealthKitActive = false;

    public Transform target;

    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset from the target.
    public float followSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsIdle", true);

    }

    // Update is used for physics operations
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", true);
        }

        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        if (isGrounded && canJump && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRunning", false);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract(); // Pass the player reference as an argument.
        }

        if (currentHealth <= 0 && !hasRevived)
        {
            ReviveInCurrentStage(true, 1f);
            hasRevived = true; // Set the flag to indicate that the PowerUp has been used.
        }

        if (target != null)
        {
            Vector3 targetPosition = target.position;
            Vector3 cameraPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.position = cameraPosition;
        }

        if (isPoisoned)
        {
            // Inflict continuous damage over time while the player is poisoned
            TakeDamage(poisonDamagePerSecond * Time.deltaTime);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnJumpAnimationEnd()
    {
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true;
        }
        if (isInvincible)
        {
            return; 
        }
        else
        {
            // Handle normal collision logic when not invincible (e.g., take damage).
            if (collision.gameObject.CompareTag("Meteor1"))
            {
                float damagePercentage = 15f;
                int damageAmount = Mathf.RoundToInt(maxHealth * (damagePercentage / 100f));

                // Deduct the calculated damage from the player's current health.
                currentHealth -= damageAmount;

                // Check if the player's health has fallen to or below zero.
                if (currentHealth <= 0)
                {
                    // The player has died. Implement any death logic here.
                    Die();
                }
            }
            else if (collision.gameObject.CompareTag("Meteor2"))
            {
                float damagePercentage = 35f;
                int damageAmount = Mathf.RoundToInt(maxHealth * (damagePercentage / 100f));

                // Deduct the calculated damage from the player's current health.
                currentHealth -= damageAmount;

                // Check if the player's health has fallen to or below zero.
                if (currentHealth <= 0)
                {
                    // The player has died. Implement any death logic here.
                    Die();
                }
            }
            else if (collision.gameObject.CompareTag("Spiketrap"))
            {
                // Calculate the damage amount (20% of current health).
                float damagePercentage = 20f;
                int damageAmount = Mathf.RoundToInt(maxHealth * (damagePercentage / 100f));

                // Deduct the calculated damage from the player's current health.
                currentHealth -= damageAmount;

                // Check if the player's health has fallen to or below zero.
                if (currentHealth <= 0)
                {
                    // The player has died. Implement any death logic here.
                    Die();
                }
            }
            else if (collision.gameObject.CompareTag("Trapmine"))
            {
                float damagePercentage = 45f;
                int damageAmount = Mathf.RoundToInt(maxHealth * (damagePercentage / 100f));

                // Deduct the calculated damage from the player's current health.
                currentHealth -= damageAmount;

                // Check if the player's health has fallen to or below zero.
                if (currentHealth <= 0)
                {
                    // The player has died. Implement any death logic here.
                    Die();
                }
            }
            else if (collision.gameObject.CompareTag("Slowmine"))
            {
                float moveSpeed = 2.5f;
            }

            else if (collision.gameObject.CompareTag("Poisonmine"))
            {
                float moveSpeed = 3.5f;
                int damageAmount = healthController.TakeDamage2(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthKit"))
        {
            // Check if the player's health is not already at full health.
            if (currentHealth < maxHealth)
            {
                // Heal the player by 10% of maximum health.
                int healingAmount = (int)(maxHealth * 0.1f);
                currentHealth = Mathf.Min(maxHealth, currentHealth + healingAmount);

                // Deactivate the HealthKit object (Power Up) after being picked up.
                other.gameObject.SetActive(false);
            }
        }
    }

    void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, interactionRange);
        if (hit.collider != null)
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                interactableObject.Interact(this);
            }
        }
    }
    //For PoisonMine
    public void TakeDamage2(float damage)
    {
        int damageAmount = Mathf.RoundToInt(currentHealth - (damage);
        currentHealth -= damageAmount; 
    }

    public void TakeDamage(float damagePercentage)
    {
        int damageAmount = Mathf.RoundToInt(maxHealth * (damagePercentage / 100f));

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You're too noob to play this game!");
        ResetPlayertoStage1(); 
    }

    public void KillPlayer()
    {
        ResetPlayertoStage1();
    }

    public void ResetPlayertoStage1()
    {
        // Reset the player's position to the initial position.
        transform.position = initialPosition;

        // Reset the player's health to the maximum.
        currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ActivatePowerUp(PowerUpTypeEnum powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpTypeEnum.Invincibility:
                StartCoroutine(ActivateInvincibility());
                break;
                // Handle other power-up cases here.
        }
    }

    public IEnumerator ActivateInvincibility()
    {
        isInvincible = true;

        // Wait for the specified duration.
        yield return new WaitForSeconds(invincibilityDuration);

        // Disable invincibility after the duration has passed.
        isInvincible = false;
    }

    public IEnumerator ActivateMeteorResistance(float duration)
    {
        if (!isMeteorResistanceActive)
        {
            // Activate Meteor Resistance.
            isMeteorResistanceActive = true;
            meteorResistanceDuration = duration;

            float originalMeteor1Damage = 0f; // 10% of max health
            float originalMeteor2Damage = 0.3f; // 30% of max health
            float meteor1Damage = isMeteorResistanceActive ? 0f : originalMeteor1Damage;
            float meteor2Damage = isMeteorResistanceActive ? originalMeteor2Damage : originalMeteor2Damage;

            // You can also modify how meteors' damage is calculated while this power-up is active.

            // Wait for the specified duration.
            yield return new WaitForSeconds(duration);

            // Meteor Resistance has expired, deactivate it.
            isMeteorResistanceActive = false;

            // Add any logic to revert any changes made while Meteor Resistance was active, if necessary.
        }
    }


    public void FakeTeleportationBacktoBeginning()
    {
        transform.position = initialPosition;
        currentHealth =  (int) (maxHealth * 0.5f);
    }

    public void ActivateSprint(float duration)
    {
        if (!isSprinting)
        {
            StartCoroutine(DoSprint(duration));
        }
    }

    private IEnumerator DoSprint(float duration)
    {
        // Store the normal movement speed.
        normalMoveSpeed = moveSpeed;

        // Increase the player's movement speed.
        moveSpeed *= sprintMoveSpeed;
        isSprinting = true;

        // Wait for the specified duration.
        yield return new WaitForSeconds(duration);

        // Restore the normal movement speed.
        moveSpeed = normalMoveSpeed;
        isSprinting = false;
    }

    public void ActivateFakeSprint()
    {
        if (!isFakeSprintActive)
        {
            originalMoveSpeed = moveSpeed; // Store the original move speed.
            moveSpeed *= 0.7f; // Reduce the movement speed by 30%.
            isFakeSprintActive = true;

            // Coroutine to revert to normal speed after 30 seconds.
            StartCoroutine(RevertToNormalSpeed(30f));
        }
    }


    private IEnumerator RevertToNormalSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        // Revert to the original movement speed.
        moveSpeed = originalMoveSpeed;
        isFakeSprintActive = false;
    }

    public void ActivateFakeHealthKit()
    {
        if (!isFakeHealthKitActive)
        {
            isFakeHealthKitActive = true;
            StartCoroutine(DecreaseHealthOverTime(15f, 5f));
        }
    }

    private IEnumerator DecreaseHealthOverTime(float duration, float interval)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            // Calculate how much health to decrease in each interval.
            float healthToDecrease = maxHealth * 0.05f; // 5% of maximum health.

            // Decrease the player's health.
            currentHealth -= (int)healthToDecrease;

            // Ensure the health doesn't go below 0.
            currentHealth = Mathf.Max(0, currentHealth);

            // Wait for the specified interval.
            yield return new WaitForSeconds(interval);
        }

        // Deactivate the FakeHealthKit effect after the specified duration.
        isFakeHealthKitActive = false;
    }
    








}
