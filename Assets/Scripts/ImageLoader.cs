using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image img;
    //private string url;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadImage());
    }

    // Update is called once per frame
    IEnumerator loadImage()
    {
        yield return new WaitForSeconds(1.5f);
        string url = TeacherLevelScript.imageURL.ToString();
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log(url);
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
            Texture2D texture2d = new Texture2D(10, 10);
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

}
