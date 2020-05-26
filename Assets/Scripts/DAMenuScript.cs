using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DAMenuScript : MonoBehaviour
{
    /* public Button stage1But; 
     public Button stage2But;
     public Button stage3But;
     public Button stage4But;
     public Button stage5But;
     public Button stage6But;
     public Button stage7But;
     public Button stage8But;
     public Button stage9But;
     public Button stage10But;
     public Button stage11But;
     public Button stage12But;
     public Button stage13But;
     public Button stage14But;
     public Button stage15But;
     public Button stage16But;
     public Button stage17But;
     public Button stage18But;
     public Button stage19But;
     public Button stage20But;
     public Button stage21But;
     public Button stage22But;
     public Button stage23But;
     public Button stage24But;
     public Button stage25But;

     public void Start()
     {
         var currentStage = DBManager.currentDAStage;
         int convStage = int.Parse(currentStage);

             if (convStage == 2 )
             {
                 stage2But.interactable = true;

             }

             else if (convStage == 3)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
             }

             else if (convStage == 4)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
             }

             else if (convStage == 5)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
             }

            else if (convStage == 6)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
             }

            else if (convStage == 7)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
             }

             else if (convStage == 8)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
             }

             else if (convStage == 9)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
             }

             else if (convStage == 10)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
             }

             else if (convStage == 11)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
         }

             else if (convStage == 12 )
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
             }

             else if (convStage == 13)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
             }

             else if (convStage == 14)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
             }

             else if (convStage == 15)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
             }

            else if (convStage == 16)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
             }

            else if (convStage == 17)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
             }

             else if (convStage == 18)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
             }

             else if (convStage == 19)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
             }

             else if (convStage == 20)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
             }

             else if (convStage == 21)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
                 stage21But.interactable = true;
             }

             else if (convStage == 22)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
                 stage21But.interactable = true;
                 stage22But.interactable = true;
             }

             else if (convStage == 23)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
                 stage21But.interactable = true;
                 stage22But.interactable = true;
                 stage23But.interactable = true;
             }

             else if (convStage == 24)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
                 stage21But.interactable = true;
                 stage22But.interactable = true;
                 stage23But.interactable = true;
                 stage24But.interactable = true;
             }

             else if (convStage == 25)
             {
                 stage2But.interactable = true;
                 stage3But.interactable = true;
                 stage4But.interactable = true;
                 stage5But.interactable = true;
                 stage6But.interactable = true;
                 stage7But.interactable = true;
                 stage8But.interactable = true;
                 stage9But.interactable = true;
                 stage10But.interactable = true;
                 stage11But.interactable = true;
                 stage12But.interactable = true;
                 stage13But.interactable = true;
                 stage14But.interactable = true; 
                 stage15But.interactable = true;
                 stage16But.interactable = true;
                 stage17But.interactable = true;
                 stage18But.interactable = true;
                 stage19But.interactable = true;
                 stage20But.interactable = true;
                 stage21But.interactable = true;
                 stage22But.interactable = true;
                 stage23But.interactable = true;
                 stage24But.interactable = true;
                 stage25But.interactable = true;
             }


         else
         {
             Debug.Log("Stage 2 not unlocked");
             Debug.Log("Stage 3 not unlocked");
         }
     }*/

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
        var currentStage = DBManager.currentDAStage;
        int convStage = int.Parse(currentStage);

        int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

        if (convStage >= stageLevel )
        {
            stageButton.interactable = true;
            StartCoroutine(checkStars());
            
        }

        else
        {
            StartCoroutine(checkStars());
        }
    }

    IEnumerator checkStars()
    {
        //string currentScene = SceneManager.GetActiveScene().name;
        //int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
        

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
                //SceneManager.LoadScene(loadMenu);
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
