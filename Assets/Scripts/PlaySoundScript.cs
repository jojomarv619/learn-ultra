using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundScript : MonoBehaviour
{
    
    public AudioSource wordSound;

    public void playSound()
    {
        wordSound.Play();
    }

}
