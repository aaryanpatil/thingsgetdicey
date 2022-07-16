
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [Header("Horizontal Movement")]

    [SerializeField] float runSpeed = 10f;
    [SerializeField] private float smoothInputSpeed = 0.08f;
    
    Vector2 moveInput;
    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 14f;
    [SerializeField] int jumpCount = 0;
    [SerializeField] int maxJumps = 2;

    [Header("Player Death")]
    [SerializeField] float delayDeath = 0.3f;
    [SerializeField] float deathVelocityY = 20f;

    [Header("Randomness")]
    [SerializeField] float gravityIncrease; 
    [SerializeField] float gravityDecrease; 
    [SerializeField] float gravityNormal;
    [SerializeField] float gravityReverse;

    [SerializeField] float runSpeedIncrease; 
    [SerializeField] float runSpeedDecrease;
    [SerializeField] float runSpeedNormal;
    [SerializeField] PhysicsMaterial2D sticky;
    [SerializeField] PhysicsMaterial2D slippery;

    Rigidbody2D rb2d; 
    BoxCollider2D boxCollider2D;
    CircleCollider2D circleCollider2D;
    Animator animator;

    
    Timer timer;

    SessionManager sessionManager;


    private void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();   
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        timer = FindObjectOfType<Timer>();
        animator = GetComponent<Animator>(); 
        sessionManager = FindObjectOfType<SessionManager>();  
    }

    private void Start() 
    {
        InvokeRepeating("StartRandomness", timer.timerDuration, timer.timerDuration); 
    }

    private void FixedUpdate() 
    {
        Run();
    }

    public void ProcessDeath()
    {
        rb2d.AddForce(new Vector2 (0, deathVelocityY), ForceMode2D.Impulse);
        boxCollider2D.enabled = false; 
        animator.SetTrigger("Death");
        Destroy(gameObject, delayDeath);

        sessionManager.ReloadLevel();
    }

    void TweakFriction()
    {
        int fac = 0;
        fac = RandomNumberGenerator.GetInt32(1, 7);
        Debug.Log("Speed: " + fac);

        if(fac < 3)
        {
            boxCollider2D.sharedMaterial = sticky;
            jumpCount = 0;
            return;
        }
        else 
        {
            boxCollider2D.sharedMaterial = slippery;
            return;
        }
    }

    void TweakSpeed()
    {
        int fac = 0;
        fac = RandomNumberGenerator.GetInt32(1, 4);
        Debug.Log("Speed: " + fac);

        if(fac == 1)
        {
            runSpeed = runSpeedIncrease;
            return;
        }
        else if(fac == 2)
        {
            runSpeed = runSpeedDecrease;
            return;
        }
        else
        {
            runSpeed = runSpeedNormal;
        }
    }

    void TweakGravity()
    {
        int fac = 0;
        fac = RandomNumberGenerator.GetInt32(1, 15);
        Debug.Log("Gravity: " + fac);

        if(fac < 4)
        {
            rb2d.gravityScale = gravityIncrease;
            return;
        }
        else if(fac < 7)
        {
            rb2d.gravityScale = gravityDecrease;
            return;
        }
        else if(fac < 10)
        {
            rb2d.gravityScale = gravityReverse;
        }
        else
        {
            rb2d.gravityScale = gravityNormal;
        }
    }

    void StartRandomness()
    {
        TweakGravity();
        TweakSpeed();
        TweakFriction();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("Hazard"))
        {
            ProcessDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            ProcessDeath();
        }
    }

    void Run()
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 playerVelocity = new Vector2(currentInputVector.x * runSpeed *  Time.fixedDeltaTime, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;     
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if(moveInput.x > 0 || moveInput.x < 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {   
            animator.SetBool("Running", false);
        }
    }

    void OnJump(InputValue value)
    {
        jumpCount++;
        if (value.isPressed && jumpCount <= maxJumps)
        {
            animator.SetBool("Running", true);
            if(rb2d.velocity.y < 0)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce - rb2d.velocity.y), ForceMode2D.Impulse);
            }
            else if(rb2d.velocity.y > 0)
            {
               rb2d.AddForce(new Vector2(0f, jumpForce - rb2d.velocity.y), ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
        else
        {
            animator.SetBool("Running", false); 
        }
    }   
}
