using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript_Rewards : MonoBehaviour
{
    
 public void backButton(string sceneName)
    {
        StartCoroutine(delayBack(sceneName));
    }

    IEnumerator delayBack(string sceneName)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName);
    }
}
