using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class keys : MonoBehaviour
{
    public Player player;
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _audio.Play();
            Destroy(gameObject);
            player.CollectKey();
        }

    }
}
