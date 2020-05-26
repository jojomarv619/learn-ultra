using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameMenuScriptNew : MonoBehaviour
{
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


            }


        }

        else if (SceneManager.GetActiveScene().name == ("FR_Menu"))
        {
            var currentStage = DBManager.currentFRStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;

            }

        }

        else if (SceneManager.GetActiveScene().name == ("SP_Menu1"))
        {
            var currentStage = DBManager.currentSPStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;


            }


        }

        else if (SceneManager.GetActiveScene().name == ("SP_Menu2"))
        {
            var currentStage = DBManager.currentSPStage;
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;


            }


        }

        else if (SceneManager.GetActiveScene().name == ("LS_Menu1"))
        {
            var currentStage = DBManager.currentLSStage;
            
            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;


            }


        }

        else if (SceneManager.GetActiveScene().name == ("LS_Menu2"))
        {
            var currentStage = DBManager.currentLSStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;

            }


        }

        else if (SceneManager.GetActiveScene().name == ("RC_Menu"))
        {
            var currentStage = DBManager.currentRCStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;

            }

        }

        else if (SceneManager.GetActiveScene().name == ("GR_Menu1"))
        {
            var currentStage = DBManager.currentGRStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;

            }

        }

        else if (SceneManager.GetActiveScene().name == ("GR_Menu2"))
        {
            var currentStage = DBManager.currentGRStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;


            }


        }

        else if (SceneManager.GetActiveScene().name == ("TT_Menu"))
        {
            var currentStage = DBManager.currentTTStage;

            int convStage = int.Parse(currentStage);

            int stageLevel = int.Parse(loadScene.Substring(loadScene.LastIndexOf('_') + 1));

            if (convStage >= stageLevel)
            {
                stageButton.interactable = true;


            }
        }

    }


    public void enterStage()
    {

        SceneManager.LoadScene(loadScene);
    }
}

