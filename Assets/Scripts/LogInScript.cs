using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class LogInScript : MonoBehaviour
{
    // Start is called before the first frame update
    /* IEnumerator Start()
     {
         WWW request = new WWW("http://localhost/webtest.php");
         yield return request;
         Debug.Log(request.text);
     } */

    public InputField userField;
    public Button submitbutton;
    [SerializeField] public AudioSource invalidFX;
    [SerializeField] public GameObject invalidGO;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

  /*  IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userField.text);
        WWW www = new WWW("http://localhost/webtest.php", form);
        yield return www;

        Debug.Log(www.text);
        if (www.text[0] == '0')
        {
            DBManager.userid = userField.text;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        }

        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    } */

  IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                SceneManager.LoadScene("Log_In");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text == "Login successful")
                {
                    DBManager.userid = userField.text;
                    StartCoroutine(getProgressInfo());
                    
                                                       
                }

                else
                {
                    //SceneManager.LoadScene("Log_In");
                    //Debug.Log("Try again");
                    yield return new WaitForSeconds(0.2f);
                    //buyButton.interactable = false;
                    invalidGO.SetActive(true);
                    invalidFX.Play();
                    yield return new WaitForSeconds(1f);
                    invalidGO.SetActive(false);
                    //buyButton.interactable = true;

                }
                
            }
        }
    }

  IEnumerator getProgressInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/getprogressdata.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                SceneManager.LoadScene("Log_In");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    var orgText = text;
                    DBManager.currentTTStage = orgText.Split('|')[0];
                    DBManager.currentSCStage = orgText.Split('|')[1];
                    DBManager.currentINStage = orgText.Split('|')[2];
                    DBManager.currentDAStage = orgText.Split('|')[3];
                    DBManager.currentFRStage = orgText.Split('|')[4];
                    DBManager.currentSPStage = orgText.Split('|')[5];
                    DBManager.currentLSStage = orgText.Split('|')[6];
                    DBManager.currentGRStage = orgText.Split('|')[7];
                    DBManager.currentRCStage = orgText.Split('|')[8];

                    SceneManager.LoadScene("Main_Menu");
                }

                else
                {
                    Debug.Log("Progress not read.");

                }

            }
        }
    }
    public void VerifyInput()
    {
        submitbutton.interactable = (userField.text.Length >= 3);
    }
}
