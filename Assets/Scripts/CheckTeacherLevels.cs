using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckTeacherLevels : MonoBehaviour
{
    [SerializeField] Text tlText;
    [SerializeField] Button tlButton;

    void Start()
    {
        StartCoroutine(checkTeacherLevels());

       
    }

    IEnumerator checkTeacherLevels()
    {
        WWWForm form = new WWWForm();
        form.AddField("class", DBManager.userClass);
        form.AddField("userid", DBManager.userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/teacherlevelcontent.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //SceneManager.LoadScene("Log_In");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text == "finished")
                {
                    tlText.text = "You have completed the activities prepared by your teacher!";
                    
                }

                else
                {
                    tlText.text = "There are activities you haven't played yet!";
                    tlButton.interactable = true;
                }

            }
        }

    }

    public void startTeacherLevel()
    {
        SceneManager.LoadScene("Teacher_Levels");
    }
}
