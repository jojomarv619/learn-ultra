using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterSoundScript : MonoBehaviour
{
    public static int Try;
    private int countGuess;

    public void wrongButton()
    {
        Try += 0;
        CompareGuesses(Try);
        countGuess++;
    }

    public void correctButton()
    {
        Try += 1;
        CompareGuesses(Try);
        countGuess++;
    }

    void CompareGuesses(int guess)
    {
        if (guess >= 1)
        {
            if (countGuess == 1)
            {
                Debug.Log("Yay you get three stars!");
            }

            else if (countGuess > 5)
            {
                Debug.Log("Yay you get one star!");
            }

            else if (countGuess > 1 && countGuess < 5)
            {
                Debug.Log("Yay you get two stars!");
            }
        }

        else
        {
            Debug.Log("Try again");
        }
    }
}
