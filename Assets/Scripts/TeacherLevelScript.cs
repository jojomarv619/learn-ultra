using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TeacherLevelScript : MonoBehaviour
{
   // [SerializeField] private string answer;//answer 

    private int countGuess; //counts how many tries the user took


    [SerializeField] private InputField input;

    //private string gameType;

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

    [SerializeField] public Text instructionsText;
    [SerializeField] public Image contentImage;
    private static string instructions, gameType, game_id, answer, avail;
    public static string imageURL;
    //public Image img; 
    



    //  void Awake() 
    //  {
    //    input = GameObject.Find("InputField").GetComponent<InputField>();   

    //num = 6; //value of answer
    //   }




    public void Start()
    {
            showSentence();
            input.text = "";
            StartCoroutine(getContent());
           // StartCoroutine(loadImage());
            
            //StartCoroutine(set)

        IEnumerator getContent()
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
                    SceneManager.LoadScene("Log_In");
                }

                else
                {
                    string text = www.downloadHandler.text;
                    Debug.Log(www.downloadHandler.text);

                    if (text != null)
                    {
                        var orgText = text;
                        imageURL = orgText.Split('|')[0];
                        instructions = orgText.Split('|')[1];
                        gameType = orgText.Split('|')[2];
                        game_id = orgText.Split('|')[3];
                        answer = orgText.Split('|')[4];
                        avail = orgText.Split('|')[5];

                        //
                        instructionsText.text = instructions;



                    }

                    else
                    {
                        //SceneManager.LoadScene("Log In");
                        Debug.Log("Try again");
                    }

                }
            }
        }

      /*  IEnumerator loadImage()
        {
            //string url = TeacherLevelScript.imageURL.ToString();
            yield return new WaitForSeconds(2);
            UnityWebRequest www = UnityWebRequest.Get(imageURL);
            Debug.Log(imageURL);
            DownloadHandler handle = www.downloadHandler;
            
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log("Error while Receiving: " + www.error);
            }
            else
            {
                Debug.Log("Success");

                //Load Image
                Texture2D texture2d = new Texture2D(8, 8);
                Sprite sprite = null;
                if (texture2d.LoadImage(handle.data))
                {
                    sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);
                }
                if (sprite != null)
                {
                    img.sprite = sprite;
                }
            }
        }
        */
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

    /*IEnumerator checkStars()
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
    */



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


                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", game_id);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 3);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 150);
                    //form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttldata.php", form))
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
                                yield return new WaitForSeconds(2f);
                                DBManager.TLevel = true;
                                completeFX.Play();                            
                            
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar3.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);




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

                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", game_id);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 1);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 50);
                   // form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttldata.php", form))
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
                                yield return new WaitForSeconds(2f);
                                DBManager.TLevel = true;
                                completeFX.Play();

                                
                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);
                                



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

                    //scoring system
                    WWWForm form = new WWWForm();
                    form.AddField("userid", DBManager.userid);
                    form.AddField("gameid", game_id);
                    form.AddField("gametype", gameType);
                    form.AddField("gamestars", 2);
                    form.AddField("attempts", countGuess);
                    form.AddField("currreward", 100);
                    //form.AddField("currhigh", newPPStreak);



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttldata.php", form))
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
                                yield return new WaitForSeconds(2f);
                                DBManager.TLevel = true;
                                completeFX.Play();

                                pStar1.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                pStar2.color = new Color(240 / 255f, 230 / 255f, 71 / 255f);
                                nextPanel.SetActive(true);
                                
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
                form.AddField("gameid", game_id);
                form.AddField("gametype", gameType);
                form.AddField("answer", guess.ToString().ToUpper());
                form.AddField("canswer", answer.ToString().ToUpper());


                using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttlattempt.php", form))
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



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttldata.php", form))
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



                    using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/inserttldata.php", form))
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
            DBManager.playPlusMode = false;
            input.text = "";
            SceneManager.LoadScene(loadMenu);
        }

        else
        {

            input.text = "";
            DBManager.playPlusMode = false;
            SceneManager.LoadScene(loadMenu);
        }


    }



    public void nextButton()
    {
        //Debug.Log(DBManager.currentActiveStage);
        //buttonClick.Play();

            input.text = "";
        //int index = Random.Range(17, 291);
        // SceneManager.LoadScene(index);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(checkTeacherLevels());

        IEnumerator checkTeacherLevels()
        {
            yield return new WaitForSeconds(5f);
            WWWForm form = new WWWForm();
            form.AddField("class", DBManager.userClass);

            using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/teacherlevelcontent.php", form))
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

                    if (text == "finished" || DBManager.TLevel == true)
                    {
                        SceneManager.LoadScene("TL_Menu");
                    }

                    else
                    {
                        //SceneManager.LoadScene("Log In");
                        DBManager.TLevel = false;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        
                    }

                }
            }

        }

    }
}


