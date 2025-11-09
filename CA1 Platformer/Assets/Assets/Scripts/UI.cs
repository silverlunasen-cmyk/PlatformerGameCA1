using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text keyText;
    public Image[] image;
    public void KeysUpdate(int keysCollected, int totalKeys)
    {
        keyText.text = keysCollected + "/" + totalKeys;
    }

    public void UpdateLives(int lives)
    {
        if (lives == 3)
        {
            image[0].enabled = true;
            image[1].enabled = true;
            image[2].enabled = true;
        }
        else if (lives == 2)
        {
            image[0].enabled = false;
            image[1].enabled = true;
            image[2].enabled = true;
        }
        else if (lives == 1)
        {
            image[0].enabled = false;
            image[1].enabled = false;
            image[2].enabled = true;
        }
        else if (lives == 0)
        {
            image[0].enabled = false;
            image[1].enabled = false;
            image[2].enabled = false;
        }
    }
}