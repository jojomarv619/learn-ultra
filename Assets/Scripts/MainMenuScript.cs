using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] public AudioSource invalidFX;
    [SerializeField] public GameObject invalidGO; 
    public void Start()
    {
        StartCoroutine(getUserInfo());
    }

    IEnumerator getUserInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/getuserinfo.php", form))
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
                //Debug.Log(www.downloadHandler.text);

                if (text != null)
                {

                    var orgText = text;
                    DBManager.userFname = orgText.Split('|')[0];
                    DBManager.userCurrHigh = orgText.Split('|')[1];
                    DBManager.userCurrency = orgText.Split('|')[2];
                    DBManager.userClass = orgText.Split('|')[3];
                    Debug.Log(DBManager.userFname +" "+ DBManager.userCurrHigh +" "+ DBManager.userCurrency+" "+DBManager.userClass);
                }

                else
                {
                    //SceneManager.LoadScene("Log In");
                    Debug.Log("Try again");
                }

            }
        }
    }

    public void Button(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //Debug.Log(DBManager.playPlusMode);
    }

    public void speButton(string sceneName)
    {
        if (int.Parse(DBManager.currentTTStage)  < 25  || int.Parse(DBManager.currentSCStage) < 25 || int.Parse(DBManager.currentINStage) < 25 || int.Parse(DBManager.currentDAStage) < 25 ||
            int.Parse(DBManager.currentFRStage) < 25 || int.Parse(DBManager.currentSPStage) < 50 || int.Parse(DBManager.currentLSStage) < 50 || int.Parse(DBManager.currentGRStage) < 50 ||
            int.Parse(DBManager.currentRCStage) < 25)
        {
            StartCoroutine(invalidAction());
        }

        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator invalidAction()
    {
        yield return new WaitForSeconds(0.2f);
        //buyButton.interactable = false;
        invalidGO.SetActive(true);
        invalidFX.Play();
        yield return new WaitForSeconds(1f);
        invalidGO.SetActive(false);
    }
}
