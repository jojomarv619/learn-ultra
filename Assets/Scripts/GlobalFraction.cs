using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFraction : MonoBehaviour
{

    public static int FractionCount;
    public GameObject CountDisplay;
    public int InternalFraction;
    private int countGuess;
    [SerializeField]
    private int num; //answer 

    void Update()
    {
        InternalFraction = FractionCount;
        CountDisplay.GetComponent<Text>().text = "Items: " + InternalFraction;

    }



    public void SubmitAnswer()
    {
       CompareGuesses(FractionCount);
       countGuess++;
    }

    void CompareGuesses(int guess)
    {
        if (guess == num)
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
