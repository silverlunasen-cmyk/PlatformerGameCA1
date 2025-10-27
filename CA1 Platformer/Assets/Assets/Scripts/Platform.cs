using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float speed; // how fast the platform is moving between point a and b;
    public int startingPoint; //where the platform initially starts;
    public Transform[] points; // this is an array of where the seperate parts the platform must go to is.



    private int index; //index of array;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[index].position) < 0.02f)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);//this sets it so that when the player collides with the platform, they become a child of the platform object meaning it will follow on the top of the platform
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);//this reverses it so that when you leave the platform you aren't stuck following it;
    }
}
