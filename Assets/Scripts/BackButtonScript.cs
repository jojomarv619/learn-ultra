using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    
 public void backButton(string sceneName)
    {
       SceneManager.LoadScene(sceneName);
    }


}
