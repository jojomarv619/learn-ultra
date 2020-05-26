using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractionScript : MonoBehaviour
{



    private bool buttonPressed = true;
    public Button fractionButton;

    public AudioSource buttonClick;
    public AudioSource buttonUnclick;

    public void pressButton()
    {
        Color c = fractionButton.image.color;


        if (buttonPressed)
        {
            GameControllerForFractionScript.fractionAnswer += 1;
            buttonPressed = false;
            c.a = 0.2f;
            fractionButton.image.color = c;
            buttonClick.Play();
            
        }

        else
        {
            GameControllerForFractionScript.fractionAnswer -= 1;
            buttonPressed = true;
            c.a = 1f;
            fractionButton.image.color = c;
            buttonUnclick.Play();
        }

    }

 
}
