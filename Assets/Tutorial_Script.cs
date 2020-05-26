using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial_Script : MonoBehaviour
{
    public RawImage rawimg;
    public VideoPlayer vidTut;
    //public AudioSource vidAudio;
    public GameObject vidPanel;
    public Button playBut, stopBut; 

   
    void Start()
    {
        if (DBManager.currentDAStage == "1" || DBManager.currentFRStage == "1" || DBManager.currentGRStage == "1" || DBManager.currentINStage == "1" || DBManager.currentLSStage == "1" ||
            DBManager.currentRCStage == "1" || DBManager.currentSCStage == "1" || DBManager.currentSPStage == "1" || DBManager.currentTTStage == "1")
        {
            vidPanel.SetActive(true);
            StartCoroutine(PlayVideo());
        }


    }



    IEnumerator PlayVideo()
    {
        vidTut.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!vidTut.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawimg.texture = vidTut.texture;
        vidTut.Play();
        vidTut.loopPointReached += stopVid; 
        //vidAudio.Play();
    }

    public void playVid()
    {
        vidPanel.SetActive(true);
        StartCoroutine(PlayVideo());
    }

    public void stopVid(VideoPlayer vp)
    {
        vidTut.Stop();
        //vidAudio.Stop();
        vidPanel.SetActive(false);
    }

}
