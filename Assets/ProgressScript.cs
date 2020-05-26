using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
 [SerializeField] Text TTText, DAText, FRText, SCText, INText, GRText, SPText, LSText, RCText;

    public void Update()
    {
        TTText.text = "Telling Time: " + DBManager.currentTTStage + "/25";
        DAText.text = "Dynamic Addition: " + DBManager.currentDAStage + "/25";
        FRText.text = "Fractions: " + DBManager.currentFRStage + "/25";
        SCText.text = "Skip Counting: " + DBManager.currentSCStage + "/25";
        INText.text = "Indetifying Numbers: " + DBManager.currentINStage + "/25";
        GRText.text = "Grammar: " + DBManager.currentGRStage + "/50";
        SPText.text = "Spelling: " + DBManager.currentSPStage + "/50";
        LSText.text = "Letter Sounds: " + DBManager.currentLSStage + "/50";
        RCText.text = "Reading Comprehension: " + DBManager.currentRCStage + "/25";
    }
}
