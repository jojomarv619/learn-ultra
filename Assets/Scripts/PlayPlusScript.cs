using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PlayPlusScript : MonoBehaviour
{
    [SerializeField]
    public Text playptext; 

    public void startPlayPlusMode()
    {
        DBManager.playPlusMode = true;
        int index = Random.Range(24, 323);
        SceneManager.LoadScene(index);
    }

    public void Start()
    {
        StartCoroutine(getUserInfo());
        playptext.text = "Hi! " +DBManager.userFname+ ", your highest streak was " +DBManager.userCurrHigh+"!";
        DBManager.playPlusHigh = 0;
        Debug.Log(DBManager.playPlusHigh);
        Debug.Log(DBManager.playPlusMode);
        
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
                    Debug.Log(www.downloadHandler.text);

                    if (text  != null)
                    {

                    var orgText = text;
                    DBManager.userFname = orgText.Split('|')[0];
                    DBManager.userCurrHigh = orgText.Substring(orgText.LastIndexOf('|') + 1);
                    
                    }

                    else
                    {
                        //SceneManager.LoadScene("Log In");
                        Debug.Log("Try again");
                    }

                }
            }
        }
    
}
