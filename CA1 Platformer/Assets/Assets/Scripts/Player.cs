using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour


{
    public int direction = 0;
    public int speed;
    private Rigidbody2D rb;
    public float JumpHeight;
    public bool jumping = false;
    private Vector2 startPosition;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;

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

        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)),
                ForceMode2D.Impulse);
            jumping = true;
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

}
