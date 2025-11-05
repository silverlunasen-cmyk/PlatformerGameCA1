using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 0.50f;
    private float destroyDelay = 0.75f;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Fall());
            }
    }

    private IEnumerator Fall()//This is a co-routine, which basically allows you to have something delayed, in this case it would be delaying the falling of the platforms.
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;//changes the rigidbody to a dynamic, meaning it will fall
        Destroy(gameObject, destroyDelay);
    }
}
