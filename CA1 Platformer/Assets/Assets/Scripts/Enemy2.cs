
using System;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Animator _animator;
    int direction = 1;
    float timeInDirection;
    public float distanceTime;
    public float speed;
    public int health;
    bool isDead = false;
    bool isIdle = false;
    public float idleTime = 2;
    float dieTime = 1;
    [SerializeField] float fireTimer = 0.5f;
    float fireCountdown = 0;
    [SerializeField] GameObject projectilePrefab;
    private AudioSource _audio;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (isIdle && idleTime < 0)
            {
                direction = direction * -1;
                _animator.SetInteger("Direction", direction);
                _animator.SetFloat("Moving", 1);
                timeInDirection = distanceTime;
                isIdle = false;
            }
            else if (!isIdle && timeInDirection < 0)
            {
                //direction = direction * -1;
                //_animator.SetInteger("Direction", direction);
                idleTime = 2;
                isIdle = true;
                _animator.SetFloat("Moving", 0);

            }
            if (!isIdle)
            {
                Vector2 pos = transform.position;
                pos.x = pos.x + (speed * Time.deltaTime * direction);
                transform.position = pos;
                timeInDirection -= Time.deltaTime;
            }
            else
            {
                idleTime -= Time.deltaTime;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                new Vector2(direction, 0), 5f, LayerMask.GetMask("Character"));
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Player>() != null)
                {
                    fire();
                }
            }
            fireCountdown -= Time.deltaTime;//how long between firing

        }
        else
        {
            dieTime -= Time.deltaTime;
            if (dieTime < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile")) ;
        {
            health--;
            if (health <= 0)
            {
                _audio.Play();
                isDead = true;
                _animator.SetBool("IsDead", true);

            }

        }

    }
    private void fire()
    {
        if (fireCountdown < 0)
        {
            fireCountdown = fireTimer;
            GameObject projectile = Instantiate(projectilePrefab
                , GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Projectile script = projectile.GetComponent<Projectile>();
            script.Launch(new Vector2(direction, 0), 300);
        }
    }
    public GameObject key;
    private void OnDestroy()
    {
        Instantiate(key, transform.position, key.transform.rotation);//when it is destroyed, it makes an instantce of something, in this case i had it so it dropped a key upon death
    }
}