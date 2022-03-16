using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private LayerMask groundLayer;
    private AudioSource audioSource;

    [Header("Horizontal Movement")]
    private float speed = 15f;
    private Vector2 direction;
    private float horizontalInput;
    private float verticalInput;
    bool facingRight = false;

    [Header("Vertical Movement")]
    private float jumpForce = 19f;
    private float jumpDelay = 0.25f;
    private float jumpTimer;
    private bool doubleJumpUsed = false;
    private float doubleJumpForce = 10;

    [Header("Physics")]
    private float maxSpeed = 15f;
    private float linearDrag = 4f;
    private float gravity = 1f;
    private float fallMultiplier = 5f;

    [Header("Collision")]
    private bool isGrounded = true;
    private float groundLength = 0.95f;
    private Vector3 colliderOffset = new Vector3(0.42f, 0f);

    //Health
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } } //Property, get = read-only to other scripts / set = writable not readable to other scripts
    bool isInvincible;
    float timeInvincible = 2.0f;
    float invincibleTimer;
    Vector2 hitEffectPosition;
    public ParticleSystem hitEffect;
    public AudioClip hitClip;

    //Respawning
    public Vector2 startPosition;
    public Vector2 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        groundLayer = LayerMask.GetMask("Ground");

        currentHealth = maxHealth;
        startPosition = transform.position;

        UpdateRespawnPosition(startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        bool wasOnGround = isGrounded;

        isGrounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (!wasOnGround && isGrounded)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetButtonDown("Jump") && isGrounded) //GetButtonDown("Jump") = Spacebar
        {
            jumpTimer = Time.time + jumpDelay;
            //Debug.Log(jumpTimer);

            doubleJumpUsed = false;
            animator.SetBool("doubleJump", doubleJumpUsed);
            //Debug.Log("Ready to Double Jump? " + doubleJumpUsed);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            DoubleJump();
        }

        animator.SetBool("onGround", isGrounded);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontalInput, verticalInput);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    void FixedUpdate()
    {
        MoveCharacter(direction.x);

        if (jumpTimer > Time.time && isGrounded)
        {
            Jump();
        }

        ModifyPhysics();
    }

    void MoveCharacter(float horizontal)
    {
        rigidbody2d.AddForce(Vector2.right * horizontal * speed);

        Flip();

        if (Mathf.Abs(rigidbody2d.velocity.x) > maxSpeed)
        {
            rigidbody2d.velocity = new Vector2((Mathf.Sign(rigidbody2d.velocity.x) * maxSpeed), rigidbody2d.velocity.y);
        }

        animator.SetFloat("horizontal", Mathf.Abs(rigidbody2d.velocity.x));
        animator.SetFloat("vertical", rigidbody2d.velocity.y);
    }

    void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
        rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;

        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }

    void DoubleJump()
    {
        //Debug.Log("doubleJumpUsed: " + doubleJumpUsed);
        //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
        rigidbody2d.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
        animator.SetBool("doubleJump", doubleJumpUsed);
    }

    void ModifyPhysics()
    {
        bool isChangingDirection = (direction.x > 0 && rigidbody2d.velocity.x < 0) || (direction.x < 0 && rigidbody2d.velocity.x > 0);

        if (isGrounded)
        {
            if (Mathf.Abs(direction.x) < 0.4f || isChangingDirection)
            {
                rigidbody2d.drag = linearDrag;
            }
            else
            {
                rigidbody2d.drag = 0f;
            }
            rigidbody2d.gravityScale = 0;
        }
        else
        {
            rigidbody2d.gravityScale = gravity;
            rigidbody2d.drag = linearDrag * 0.15f;

            if (rigidbody2d.velocity.y < 0)
            {
                rigidbody2d.gravityScale = gravity * fallMultiplier;
            }
            else if (rigidbody2d.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                rigidbody2d.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        //replaced: void Flip()
        if (direction.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (direction.x < -0.01f)
            spriteRenderer.flipX = true;
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            //characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            //characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("hit");
            rigidbody2d.AddForce(Vector2.left * 100, ForceMode2D.Impulse);
            //hitEffectPosition = rigidbody2d.position;
            //Instantiate(hitEffect, hitEffectPosition, Quaternion.identity);
            //PlaySound(hitClip);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //Clamping ensures that the first parameter (here currentHealth + amount) never goes lower than the second parameter (here 0) and never goes above the third parameter (maxHealth). So Player’s health will always stay between 0 and maxHealth.

        if (currentHealth == 0)
            Respawn();

        Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void UpdateRespawnPosition(Vector2 position)
    {
        respawnPosition = position;
        Debug.Log("Respawn postion is " + respawnPosition);
    }

    void Respawn()
    {
        Debug.Log("Respawn and the Respawn postion is " + respawnPosition);
        ChangeHealth(maxHealth);
        transform.position = respawnPosition;
    }
}
