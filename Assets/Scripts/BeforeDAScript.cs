using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BeforeDAScript : MonoBehaviour
{
    //public Button sButton;
    
    
    public void callcheckDAstage(string sceneName)
    {
        StartCoroutine(checkDAstage(sceneName));
    }

    IEnumerator checkDAstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/daprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if ( text != null)
                {
                    DBManager.currentDAStage = text;
                    

                    SceneManager.LoadScene(sceneName);
                    Debug.Log(text);
                }

                else
                {
                    
                    Debug.Log("Try again");
                }

            }
        }
    }

    //buttons
    /*public void stageButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
    }
    */

}
