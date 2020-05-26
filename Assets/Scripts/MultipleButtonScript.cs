using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleButtonScript : MonoBehaviour
{

    //public GameObject buttonPanel;
    //public Button[] answerButtons;
    public Button answerButton;
    private bool buttonPressed = true;
    [SerializeField]
    public string buttonValue;

    public AudioSource buttonClick;
    public AudioSource buttonUnclick;

    public void pressButton()
    {
        Color c = answerButton.image.color;
        if (buttonPressed)
        {
            buttonClick.Play();
            GameControllerForLetterSoundsScript.input = buttonValue;
            GameControllerForReadingCompScript.input = buttonValue;
            GameControllerForGrammarScript.input = buttonValue;
            buttonPressed = false;
            c.a = 0.2f;
            answerButton.image.color = c;            
            answerButton.tag = "trueButtonTag";

            //disable other buttons
            GameObject[] btns = GameObject.FindGameObjectsWithTag("answerButtonTag");
            foreach (GameObject btn in btns)
                {
                btn.GetComponent<Button>().interactable = false;
                }
        } 

        else
        {
            buttonUnclick.Play();
            GameControllerForLetterSoundsScript.input = "";
            GameControllerForReadingCompScript.input = "";
            GameControllerForGrammarScript.input = "";
            buttonPressed = true;
            c.a = 1f;
            answerButton.image.color = c;
            answerButton.tag = "answerButtonTag";

            //enable buttons again
            GameObject[] btns = GameObject.FindGameObjectsWithTag("answerButtonTag");
            foreach (GameObject btn in btns)
            {
                btn.GetComponent<Button>().interactable = true;
            }
        }
    }
    

}
