using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static Cinemachine.DocumentationSortingAttribute;

public class Player : MonoBehaviour


{
    public int direction = 0;
    public int speed;
    private Rigidbody2D rb;
    public float JumpHeight;
    public bool jumping = false;
    private Vector2 startPosition;
    private Animator animator;
    public GameObject projectilePrefab;
    public float defaultPowerUpTime = 10;
    private float powerUpTimeRemaining = 10;
    private AudioSource _audio;
    public double reloadTime = 0.05;
    public int lives = 3;
    [SerializeField] private UI ui;
    public int keysCollected;
    public int totalKeys = 5;
    public bool isSpeedPowerUp = false;
    public bool isJumpPowerUp = false;
    public bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moving = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        if (position.y < -10.5)
        {
            position = startPosition;
        }
        else
        {
            position.x = position.x + (speed * Time.deltaTime * moving);
        }
        transform.position = position;
        updateAnimation(moving);

        if (Input.GetKeyDown(KeyCode.Space)&& !jumping)
        {
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)),
                ForceMode2D.Impulse);
            jumping = true;
    
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = Instantiate(projectilePrefab,
                rb.position, Quaternion.identity);
            Projectile pr = projectile.GetComponent<Projectile>();
            pr.Launch(new Vector2(animator.GetInteger("Direction"), 0), 300);
            reloadTime -= Time.deltaTime;

        }
        if (isSpeedPowerUp)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            if (powerUpTimeRemaining < 0)
            {
                isSpeedPowerUp = false;
                powerUpTimeRemaining = defaultPowerUpTime;
                animator.speed /= 2;
                speed /= 2;
                
            }

        }
        //the reason that i have them seperate is so that if you have a jump one it woudl only effect the jump and same with speed
        if (isJumpPowerUp)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            if (powerUpTimeRemaining < 0)
            {
                isSpeedPowerUp = false;
                powerUpTimeRemaining = defaultPowerUpTime;
                animator.speed /= 2;
                JumpHeight /= 2;

            }

        }
        if (keysCollected >= totalKeys && !win)
        {
            win = true;
        }
    
        if (lives == 0)
        {
            transform.position = startPosition;
            lives = 3;
        }
        if(win == true)
        {
            SceneManager.LoadScene("Victory");//loads the victory screen when you have finished;
        }
    



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      jumping = false;
    }
    private void updateAnimation(float moving)
    {
        animator.SetFloat("Moving", moving);
        if (moving > 0)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (moving < 0)
        {
            animator.SetInteger("Direction", -1);
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpeedPowerUp && collision.gameObject.tag == "SpeedPowerUp" && !isJumpPowerUp)
        {
            Destroy(collision.gameObject);
            speed = speed * 2;
            isSpeedPowerUp = true;
            animator.speed *= 2;
            _audio.pitch *=2;

        }
        if (!isJumpPowerUp && collision.gameObject.tag == "JumpPowerUp" && !isSpeedPowerUp)
        {
            Destroy(collision.gameObject);
            JumpHeight = JumpHeight * 2;
            isJumpPowerUp = true;
            animator.speed *= 2;
        }
        if (collision.gameObject.tag == "Checkpoint")
        {
            startPosition = transform.position;
        }
        if (collision.gameObject.name.Contains("EnemyProjectile"))
        {
            lives--;
            ui.UpdateLives(lives);
        }


    }
    public void CollectKey()
    {
     
        keysCollected++;
        ui.KeysUpdate(keysCollected);    
    }



}
