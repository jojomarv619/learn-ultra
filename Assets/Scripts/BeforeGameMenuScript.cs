using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BeforeGameMenuScript : MonoBehaviour
{
    //Dynamic Addition
    public void callcheckDAstage(string sceneName)
    {
        StartCoroutine(checkDAstage(sceneName));
    }

    IEnumerator checkDAstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/daprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
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

    //Identifying Numbers
    public void callcheckINstage(string sceneName)
    {
        StartCoroutine(checkINstage(sceneName));
    }

    IEnumerator checkINstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentINStage = text;


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


    //Skip Counting
    public void callcheckSCstage(string sceneName)
    {
        StartCoroutine(checkSCstage(sceneName));
    }

    IEnumerator checkSCstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/scprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentSCStage = text;


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

    //Fractions
    public void callcheckFRstage(string sceneName)
    {
        StartCoroutine(checkFRstage(sceneName));
    }

    IEnumerator checkFRstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/frprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentFRStage = text;


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

    //Spelling
    public void callcheckSPstage(string sceneName)
    {
        StartCoroutine(checkSPstage(sceneName));
    }

    IEnumerator checkSPstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/spprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentSPStage = text;


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

    //Letter Sounds
    public void callcheckLSstage(string sceneName)
    {
        StartCoroutine(checkLSstage(sceneName));
    }

    IEnumerator checkLSstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/lsprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentLSStage = text;


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

    //Reading Comprehension
    public void callcheckRCstage(string sceneName)
    {
        StartCoroutine(checkRCstage(sceneName));
    }

    IEnumerator checkRCstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/rcprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentRCStage = text;


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

    //Grammar
    public void callcheckGRstage(string sceneName)
    {
        StartCoroutine(checkGRstage(sceneName));
    }

    IEnumerator checkGRstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/grprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentGRStage = text;


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

    //Telling Time
    public void callcheckTTstage(string sceneName)
    {
        StartCoroutine(checkTTstage(sceneName));
    }

    IEnumerator checkTTstage(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/ttprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Main Menu");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    DBManager.currentTTStage = text;


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
}
