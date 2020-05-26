using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{

    public static string userid;
    public static string userFname; 
    public static string userCurrHigh; //current highest play plus streak
    public static string userCurrency;
    public static string userClass;

    public static string currentDAStage; //Dynamic Addition
    public static string currentActiveStage; 
    public static string currentINStage; //Identifying Numbers
    public static string currentSCStage; //Skip Counting
    public static string currentFRStage; //Fractions
    public static string currentSPStage; //Spelling
    public static string currentLSStage; //Letter Sounds
    public static string currentRCStage; //Reading Comp
    public static string currentGRStage; //Grammar
    public static string currentTTStage; //Telling Time

    public static bool playPlusMode = false; //checks whether user is in play plus mode
    public static int playPlusHigh = 0;
    public static bool TLevel = false;


}
