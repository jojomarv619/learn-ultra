using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameMenuScriptAlpha : MonoBehaviour
{
    // public Image star1;
    // public Image star2;
    // public Image star3;
    // public Button stageButton;
    [Header("Buttons")]
    [SerializeField] private CreateMenuButtonScript[] menuButtonA ;

    [Header("References")]
    [SerializeField] private Transform menuContainer;
    [SerializeField] private GameObject buttonPrefab;
    public string gameType;
    public string gameId;
    private string fullGameId;

    public void Start()
    {
        for (int i = 0; i < menuButtonA.Length; i++)
        {
            
            CreateMenuButtonScript but = menuButtonA[i];
            GameObject itemObject = Instantiate(buttonPrefab, menuContainer);
            fullGameId = gameId + "_" + but.stageNumber;
            itemObject.transform.GetChild(0).GetComponent<Text>().text = but.stageText;
            if (gameType == "fractions")
            {
               if  (int.Parse(DBManager.currentFRStage) < but.stageNumber)
                {
                    itemObject.GetComponent<Button>().interactable = false;
                }

               else
                {
                    StartCoroutine(loadStars());
                    itemObject.GetComponent<Button>().onClick.AddListener(() => enterStage(fullGameId));
                }
            }

            IEnumerator loadStars()
            {
                StartCoroutine(checkStars());
                yield return new WaitForSeconds(1);
              
                IEnumerator checkStars()
                {
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", fullGameId);
                    form.AddField("gametype", gameType);

                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/stageprogress.php", form))
                    {
                        yield return www.SendWebRequest();

                        //yield return new WaitForSeconds(5);
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
                                    itemObject.transform.GetChild(1).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                }

                                else if (text == "2")
                                {
                                    itemObject.transform.GetChild(1).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    itemObject.transform.GetChild(2).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                }

                                else if (text == "3")
                                {
                                    itemObject.transform.GetChild(1).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    itemObject.transform.GetChild(2).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    itemObject.transform.GetChild(3).GetComponent<Image>().color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                }

                                else if (text == "0")
                                {
                                    Debug.Log("yeet");
                                }

                            }

                            else
                            {

                                Debug.Log("Try again");
                            }

                        }
                    }
                }
            }
        }

        Debug.Log(fullGameId);
    }
    /*public void Start()
    {
        if (SceneManager.GetActiveScene().name == ("SC_Menu_Beta"))
        {
            var currentStage = DBManager.currentSCStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("IN_Menu_Beta"))
        {
            var currentStage = DBManager.currentINStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else  if (SceneManager.GetActiveScene().name == ("DA_Menu_Beta"))
        {
            var currentStage = DBManager.currentDAStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("FR_Menu_Beta"))
        {
            var currentStage = DBManager.currentFRStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("SP_Menu1_Beta"))
        {
            var currentStage = DBManager.currentSPStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("SP_Menu2_Beta"))
        {
            var currentStage = DBManager.currentSPStage
;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("LS_Menu1_Beta"))
        {
            var currentStage = DBManager.currentLSStage;
;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("LS_Menu2_Beta"))
        {
            var currentStage = DBManager.currentLSStage;
            
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("RC_Menu_Beta"))
        {
            var currentStage = DBManager.currentRCStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("GR_Menu1_Beta"))
        {
            var currentStage = DBManager.currentGRStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }

        else if (SceneManager.GetActiveScene().name == ("GR_Menu2_Beta"))
        {
            var currentStage = DBManager.currentGRStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;
               //StartCoroutine(checkStars());

            }

            else
            {
               //StartCoroutine(checkStars());
            }
        }


    } */

    /* IEnumerator checkStars()
     {



         WWWForm form = new WWWForm();
         form.AddField("userid", DBManager.userid);
         form.AddField("gameid", loadScene);
         form.AddField("gametype", gameType);

         using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/stageprogress.php", form))
         {
             yield return www.SendWebRequest();

             //yield return new WaitForSeconds(5);
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

                     else
                     {
                         yield return 0.5f;
                     }

                 }

                 else
                 {

                     Debug.Log("Try again");
                 }

             }
         }
     } */

    public void enterStage(string fullGameId)
    {
        SceneManager.LoadScene(fullGameId);
    }
}


