using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator _animator;
    public float distanceTime;
    public float speed;
    int direction;
    float timeInDirection;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
        _animator.SetFloat("Move", direction);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x * speed * Time.deltaTime * direction;
        timeInDirection -= Time.deltaTime;
        if(timeInDirection < 0)
        {
            direction *= -1;
            timeInDirection = distanceTime;
            _animator.SetInteger("Direction", direction);
        }
    }
}
