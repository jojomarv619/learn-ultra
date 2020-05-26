using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameControllerForGrammarScript2 : MonoBehaviour
{
    [SerializeField]
    private string answer; //answer 
    private int countGuess; //counts how many tries the user took

    [SerializeField]
    public InputField input;

    [SerializeField]
    private string gameType;

    [SerializeField]
    private string loadMenu;

    public Image star1;
    public Image star2;
    public Image star3;

    public Image pStar1;
    public Image pStar2;
    public Image pStar3;

    public GameObject nextPanel;
    public Text stageText;
    public AudioSource buttonClick;
    public AudioSource completeFX;

    public GameObject mistakePanel;
    public AudioSource mistakeFX;

    [SerializeField]
    private bool alwaysInclude;

    public GameObject sentencePanel;





    //  void Awake() 
    //  {
    //    input = GameObject.Find("InputField").GetComponent<InputField>();   

    //num = 6; //value of answer
    //   }




    public void Start()
    {
        if (DBManager.playPlusMode == false)
        {
            showSentence();
            input.text = "";
            StartCoroutine(checkStars());

        }

        else
        {
            StartCoroutine(getUserInfo());
            int currentPPStreak = DBManager.playPlusHigh;
            var newPPStreak = currentPPStreak + 1;
            stageText.text = "Stage " + newPPStreak;
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

                    if (text != null)
                    {
                        var orgText = text;
                        DBManager.userFname = orgText.Split('|')[0];
                        DBManager.userCurrHigh = orgText.Split('|')[1];
                        DBManager.userCurrency = orgText.Split('|')[2];

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

    public void showSentence()
    {
        if (alwaysInclude == true)
        {
            sentencePanel.SetActive(true);
        }

        else
        {
            sentencePanel.SetActive(false);
        }
    }

    public void closeSentence()
    {
        buttonClick.Play();
        sentencePanel.SetActive(false);
    }

    IEnumerator checkStars()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
        stageText.text = "Stage " + currentNumber;

        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);
        form.AddField("gameid", SceneManager.GetActiveScene().name);
        form.AddField("gametype", gameType);

        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/stageprogress.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                SceneManager.LoadScene(loadMenu);
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




    public void GetInput()
    {
        if (input.text == "")
        {
            var guess = "";
            StartCoroutine(CompareGuesses(guess));
            Debug.Log("Tries: " + countGuess);
        }

        else
        {
            var guess = input.text;
            StartCoroutine(CompareGuesses(guess));
            countGuess++;
            Debug.Log("Tries: " + countGuess);
        }

        //input.text = "";


        buttonClick.Play();
    }

    IEnumerator CompareGuesses(string guess)

    {
        if (DBManager.playPlusMode == false)
        {
            if (guess.ToUpper() == answer.ToUpper())
            {
                if (countGuess == 1)
                {

                    //Proceed to next stage
                    string currentScene = SceneManager.GetActiveScene().name;
                    int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
                    var nextNumber = currentNumber + 1;
                    var nextScene = currentScene.Split('_')[0];
                    var nextSceneNumber = nextScene + "_" + nextNumber;



                    if (nextNumber > 50)
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);
                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 3);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 150);
                        form.AddField("clevel", 50);


                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {
                                    completeFX.Play();
                                    // input = "";
                                    DBManager.currentActiveStage = loadMenu;

                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);

                                    if (gameType == "grammar")
                                    {
                                        DBManager.currentGRStage = "50";
                                    }



                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }

                    else
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);
                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 3);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 150);
                        form.AddField("clevel", nextNumber);



                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                                Debug.Log("Error");
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {
                                    completeFX.Play();
                                    // input = "";
                                    DBManager.currentActiveStage = nextSceneNumber;
                                    Debug.Log("Next stage is " + DBManager.currentActiveStage);

                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);

                                    if (gameType == "grammar")
                                    {
                                        var dbStage = int.Parse(DBManager.currentGRStage);

                                        if (dbStage >= nextNumber)
                                        {
                                            DBManager.currentGRStage = dbStage.ToString();
                                        }

                                        else
                                        {
                                            DBManager.currentGRStage = nextNumber.ToString();
                                        }
                                    }
                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }
                    //

                }

                else if (countGuess > 5)
                {
                    //Proceed to next stage
                    string currentScene = SceneManager.GetActiveScene().name;
                    int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
                    var nextNumber = currentNumber + 1;
                    var nextScene = currentScene.Split('_')[0];
                    var nextSceneNumber = nextScene + "_" + nextNumber;
                    DBManager.currentActiveStage = nextSceneNumber;


                    if (nextNumber > 50)
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);
                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 1);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 50);
                        form.AddField("clevel", 50);


                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {
                                    completeFX.Play();
                                    // input = "";
                                    DBManager.currentActiveStage = loadMenu;
                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);


                                    if (gameType == "grammar")
                                    {
                                        DBManager.currentGRStage = "grammar";
                                    }


                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }

                    else
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);

                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 1);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 50);
                        form.AddField("clevel", nextNumber);



                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                                Debug.Log("Error");
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {
                                    completeFX.Play();
                                    //input = "";
                                    DBManager.currentActiveStage = nextSceneNumber;
                                    Debug.Log("Next stage is " + DBManager.currentActiveStage);

                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);

                                    if (gameType == "grammar")
                                    {
                                        var dbStage = int.Parse(DBManager.currentGRStage);

                                        if (dbStage >= nextNumber)
                                        {
                                            DBManager.currentGRStage = dbStage.ToString();
                                        }

                                        else
                                        {
                                            DBManager.currentGRStage = nextNumber.ToString();
                                        }
                                    }

                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }
                    //
                }

                else if (countGuess > 1 && countGuess < 5)
                {
                    //Proceed to next stage
                    string currentScene = SceneManager.GetActiveScene().name;
                    int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
                    var nextNumber = currentNumber + 1;
                    var nextScene = currentScene.Split('_')[0];
                    var nextSceneNumber = nextScene + "_" + nextNumber;


                    if (nextNumber > 50)
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);
                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 2);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 100);
                        form.AddField("clevel", 50);


                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {

                                    completeFX.Play();
                                    //input = "";
                                    DBManager.currentActiveStage = loadMenu;
                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);


                                    if (gameType == "grammar")
                                    {
                                        DBManager.currentGRStage = "50";
                                    }

                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }

                    else
                    {
                        //scoring system
                        WWWForm form = new WWWForm();
                        form.AddField("userid", DBManager.userid);
                        form.AddField("gameid", SceneManager.GetActiveScene().name);
                        form.AddField("gametype", gameType);
                        form.AddField("gamestars", 2);
                        form.AddField("attempts", countGuess);
                        form.AddField("currreward", 100);
                        form.AddField("clevel", nextNumber);



                        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertgamedata.php", form))
                        {
                            yield return www.SendWebRequest();

                            if (www.isNetworkError || www.isHttpError)
                            {
                                Debug.Log(www.error);
                                SceneManager.LoadScene(loadMenu);
                                Debug.Log("Error");
                            }

                            else
                            {
                                string text = www.downloadHandler.text;
                                Debug.Log(www.downloadHandler.text);

                                if (text != null)
                                {
                                    completeFX.Play();
                                    //input = "";
                                    DBManager.currentActiveStage = nextSceneNumber;
                                    Debug.Log("Next stage is " + DBManager.currentActiveStage);
                                    pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                    nextPanel.SetActive(true);

                                    if (gameType == "grammar")
                                    {
                                        var dbStage = int.Parse(DBManager.currentGRStage);

                                        if (dbStage >= nextNumber)
                                        {
                                            DBManager.currentGRStage = dbStage.ToString();
                                        }

                                        else
                                        {
                                            DBManager.currentGRStage = nextNumber.ToString();
                                        }
                                    }

                                }

                                else
                                {

                                    Debug.Log("Try again");
                                }

                            }
                        }
                        //

                    }
                    //
                }
            }

            else if (guess == "" || guess == null)
            {
                StartCoroutine(wrongMistake());
            }

            else
            {

                StartCoroutine(wrongMistake());
                WWWForm form = new WWWForm();
                form.AddField("userid", DBManager.userid);
                form.AddField("gameid", SceneManager.GetActiveScene().name);
                form.AddField("gametype", gameType);
                form.AddField("answer", guess.ToString().ToUpper());
                form.AddField("canswer", answer.ToString().ToUpper());


                using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertattempt.php", form))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                        Debug.Log("Answer is " + answer.ToString());
                        Debug.Log("Correct Answer is " + guess.ToString());
                    }

                    else
                    {
                        string text = www.downloadHandler.text;
                        Debug.Log(www.downloadHandler.text);

                        if (text != null)
                        {
                            Debug.Log("Try Again");

                        }

                        else
                        {

                            Debug.Log("Fail");
                        }

                    }
                }
            }
        }

        else
        {
            if (guess.ToUpper() == answer.ToUpper())
            {
                if (countGuess == 1)
                {
                    //Proceed to next stage
                    int currentPPStreak = DBManager.playPlusHigh;
                    var newPPStreak = currentPPStreak + 1;

                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 2);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 300);
                    form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertppdata.php", form))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError || www.isHttpError)
                        {
                            Debug.Log(www.error);
                            SceneManager.LoadScene(loadMenu);
                            Debug.Log("Error");
                        }

                        else
                        {
                            string text = www.downloadHandler.text;
                            Debug.Log(www.downloadHandler.text);

                            if (text != null)
                            {
                                completeFX.Play();

                                DBManager.playPlusHigh = newPPStreak;
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);
                                Debug.Log("Your streak is " + newPPStreak);



                            }

                            else
                            {

                                Debug.Log("Try again");
                            }

                        }
                    }
                    //

                }

                else if (countGuess > 5)
                {
                    //Proceed to next stage
                    int currentPPStreak = DBManager.playPlusHigh;
                    var newPPStreak = currentPPStreak + 1;

                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 2);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 100);
                    form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertppdata.php", form))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError || www.isHttpError)
                        {
                            Debug.Log(www.error);
                            SceneManager.LoadScene(loadMenu);
                            Debug.Log("Error");
                        }

                        else
                        {
                            string text = www.downloadHandler.text;
                            Debug.Log(www.downloadHandler.text);

                            if (text != null)
                            {
                                completeFX.Play();

                                DBManager.playPlusHigh = newPPStreak;
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);
                                Debug.Log("Your streak is " + newPPStreak);



                            }

                            else
                            {

                                Debug.Log("Try again");
                            }

                        }
                    }
                    //
                }

                else if (countGuess > 1 && countGuess < 5)
                {
                    //Proceed to next stage
                    int currentPPStreak = DBManager.playPlusHigh;
                    var newPPStreak = currentPPStreak + 1;

                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 2);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 200);
                    form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertppdata.php", form))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError || www.isHttpError)
                        {
                            Debug.Log(www.error);
                            SceneManager.LoadScene(loadMenu);
                            Debug.Log("Error");
                        }

                        else
                        {
                            string text = www.downloadHandler.text;
                            Debug.Log(www.downloadHandler.text);

                            if (text != null)
                            {
                                completeFX.Play();

                                DBManager.playPlusHigh = newPPStreak;
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);
                                Debug.Log("Your streak is " + newPPStreak);



                            }

                            else
                            {

                                Debug.Log("Try again");
                            }

                        }
                    }
                    //


                    //
                }
            }

            else if (guess == "" || guess == null)
            {
                StartCoroutine(wrongMistake());
            }

            else
            {
                StartCoroutine(wrongMistake());
                WWWForm form = new WWWForm();
                form.AddField("userid", DBManager.userid);
                form.AddField("gameid", SceneManager.GetActiveScene().name);
                form.AddField("gametype", gameType);
                form.AddField("answer", guess.ToString().ToUpper());
                form.AddField("canswer", answer.ToString().ToUpper());


                using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertppattempt.php", form))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                        Debug.Log("Answer is " + answer.ToString());
                        Debug.Log("Correct Answer is " + guess.ToString());
                    }

                    else
                    {
                        string text = www.downloadHandler.text;
                        Debug.Log(www.downloadHandler.text);

                        if (text != null)
                        {
                            Debug.Log("Try Again");

                        }

                        else
                        {

                            Debug.Log("Fail");
                        }

                    }
                }
            }
        }

        IEnumerator wrongMistake()
        {

            yield return new WaitForSeconds(0.2f);
            mistakePanel.SetActive(true);
            mistakeFX.Play();
            yield return new WaitForSeconds(0.2f);
            mistakePanel.SetActive(false);

        }
    }

    public void goBack()
    {
        //backClick.Play();
        if (DBManager.playPlusMode == false)
        {
            input.text = "";
            DBManager.playPlusMode = false;
            SceneManager.LoadScene(loadMenu);
        }

        else
        {
            
            input.text = "";
            DBManager.playPlusMode = false;
            SceneManager.LoadScene("PlayP_Menu");
        }

        
    }



    public void nextButton()
    {
        //Debug.Log(DBManager.currentActiveStage);
        //buttonClick.Play();

        if (DBManager.playPlusMode == false)
        {
            input.text = "";
            SceneManager.LoadScene(DBManager.currentActiveStage);
        }

        else
        {
            input.text = "";
            int index = Random.Range(24, 323);
            SceneManager.LoadScene(index);
        }

    }
}


