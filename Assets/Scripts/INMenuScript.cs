using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class INMenuScript : MonoBehaviour
{
    public Image star1;
    public Image star2;
    public Image star3;
    public Button stageButton;

    [SerializeField]
    private string gameType;

    [SerializeField]
    private string loadScene;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == ("SC_Menu"))
        {
            var currentStage = DBManager.currentSCStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
                StartCoroutine(checkStars());

            }

            else
            {
                StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("IN_Menu"))
        {
            var currentStage = DBManager.currentINStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
                StartCoroutine(checkStars());

            }

            else
            {
                StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("DA_Menu"))
        {
            var currentStage = DBManager.currentDAStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
                StartCoroutine(checkStars());

            }

            else
            {
                StartCoroutine(checkStars());
            }
        }
    }

    IEnumerator checkStars()
    {



        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);
        form.AddField("gameid", loadScene);
        form.AddField("gametype", gameType);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/stageprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                    if (text == "1")
                    {
                        star1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                    }

                    else if (text == "2")
                    {
                        star1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                        star2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                    }

                    else if (text == "3")
                    {
                        star1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                        star2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                        star3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                    }

                }

                else
                {

                    Debug.Log("Try again");
                }

            }
        }
    }

    public void enterStage()
    {

        SceneManager.LoadScene(loadScene);
    }
}
