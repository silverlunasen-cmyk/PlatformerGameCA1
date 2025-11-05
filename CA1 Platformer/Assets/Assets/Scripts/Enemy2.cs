
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




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
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
        }
    }
}