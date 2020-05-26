using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameControllerForFractionScript : MonoBehaviour
{
    [SerializeField]
    private int num; //answer 
    private int countGuess; //counts how many tries the user took
    public static int fractionAnswer; //user's fraction count

 //   [SerializeField]
 //   private InputField input;

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
    //public AudioSource backClick;

    public GameObject mistakePanel;
    public AudioSource mistakeFX;




    //  void Awake() 
    //  {
    //    input = GameObject.Find("InputField").GetComponent<InputField>();   

    //num = 6; //value of answer
    //   }



    public void Start()
    {
        if (DBManager.playPlusMode == false)
        { 
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
        if (fractionAnswer == 0)
        {
            var guess = 0;
            StartCoroutine(CompareGuesses(guess));
            Debug.Log("Tries: " + countGuess);
        }
        
        else
        {
            var guess = fractionAnswer.ToString();
            StartCoroutine(CompareGuesses(int.Parse(guess)));
            countGuess++;
            Debug.Log("Tries: " + countGuess);
        }
        
        //input.text = "";
        
        buttonClick.Play();
    }

    IEnumerator CompareGuesses(int guess)

    {
        if (DBManager.playPlusMode == false)
        { 
        if (guess == num)
        {
            if (countGuess == 1)
            {

                //Proceed to next stage
                string currentScene = SceneManager.GetActiveScene().name;
                int currentNumber = int.Parse(currentScene.Substring(currentScene.LastIndexOf('_') + 1));
                var nextNumber = currentNumber + 1;
                var nextScene = currentScene.Split('_')[0];
                var nextSceneNumber = nextScene + "_" + nextNumber;



                if (nextNumber > 25)
                {
                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 3);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 150);
                    form.AddField("clevel", 25);


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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = loadMenu;
                                
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);


                                if (gameType == "fractions")
                                {
                                    DBManager.currentFRStage = "25";
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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = nextSceneNumber;
                                Debug.Log("Next stage is " + DBManager.currentActiveStage);
                                
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);

                                if (gameType == "fractions")
                                {
                                    var dbFRStage = int.Parse(DBManager.currentFRStage);

                                    if (dbFRStage >= nextNumber)
                                    {
                                        DBManager.currentFRStage = dbFRStage.ToString();
                                    }

                                    else
                                    {
                                        DBManager.currentFRStage = nextNumber.ToString();
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


                if (nextNumber > 25)
                {
                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 1);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 50);
                    form.AddField("clevel", 25);


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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = loadMenu;
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);

                                if (gameType == "fractions")
                                {
                                    DBManager.currentFRStage = "25";
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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = nextSceneNumber;
                                Debug.Log("Next stage is " + DBManager.currentActiveStage);
                                
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);

                                if (gameType == "fractions")
                                {
                                    var dbFRStage = int.Parse(DBManager.currentFRStage);

                                    if (dbFRStage >= nextNumber)
                                    {
                                        DBManager.currentFRStage = dbFRStage.ToString();
                                    }

                                    else
                                    {
                                        DBManager.currentFRStage = nextNumber.ToString();
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


                if (nextNumber > 25)
                {
                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", SceneManager.GetActiveScene().name);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 2);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 100);
                    form.AddField("clevel", 25);


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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = loadMenu;
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);

                                if (gameType == "fractions")
                                {
                                    DBManager.currentFRStage = "25";
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
                                fractionAnswer = 0;
                                DBManager.currentActiveStage = nextSceneNumber;
                                Debug.Log("Next stage is " + DBManager.currentActiveStage);
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);

                                if (gameType == "fractions")
                                {
                                    var dbFRStage = int.Parse(DBManager.currentFRStage);

                                    if (dbFRStage >= nextNumber)
                                    {
                                        DBManager.currentFRStage = dbFRStage.ToString();
                                    }

                                    else
                                    {
                                        DBManager.currentFRStage = nextNumber.ToString();
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

        else if (guess == 0)
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
            form.AddField("answer", guess.ToString());
            form.AddField("canswer", num.ToString());


            using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertattempt.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log("Answer is " + num.ToString());
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
            if (guess == num)
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

            else if (guess == 0)
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
                form.AddField("answer", guess.ToString());
                form.AddField("canswer", num.ToString());


                using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertppattempt.php", form))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                        Debug.Log("Answer is " + num.ToString());
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
            DBManager.playPlusMode = false;
            fractionAnswer = 0;
            SceneManager.LoadScene(loadMenu);
        }

        else
        {
           
            DBManager.playPlusMode = false;
            fractionAnswer = 0;
            SceneManager.LoadScene("PlayP_Menu");

        }

       
    }



    public void nextButton()
    {
        //Debug.Log(DBManager.currentActiveStage);
        //buttonClick.Play();
               if (DBManager.playPlusMode == false)
        {
            fractionAnswer = 0;
            SceneManager.LoadScene(DBManager.currentActiveStage);
        }

        else
        {
            fractionAnswer = 0;
            int index = Random.Range(24, 323);
            SceneManager.LoadScene(index);
        }
    }
}


