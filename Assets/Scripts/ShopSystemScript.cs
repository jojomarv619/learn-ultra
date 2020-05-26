using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ShopSystemScript : MonoBehaviour
{

    [Header("List of Items Sold")]
    [SerializeField] private ShopItemScript[] shopItemA;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] public GameObject buyPanel;
    [SerializeField] public AudioSource buyFX;
    [SerializeField] public Text itemNameText;
    [SerializeField] public Text itemPriceText;
    [SerializeField] public AudioSource invalidFX;
    [SerializeField] public GameObject invalidGO;
    [SerializeField] public Button buyButton;
    [SerializeField] public Text shopText;

    private string disItemName;
    private string disItemID;
    private int disItemCost;

    public string idExists;

    private void Start()
    {
        StartCoroutine(getUserInfo());
        for (int i = 0; i < shopItemA.Length; i++)
        {

            ShopItemScript si = shopItemA[i]; //shop item from array
            StartCoroutine(checkItem());


            //checkItem
            IEnumerator checkItem()
            {
                WWWForm form = new WWWForm();
                form.AddField("userid", DBManager.userid);
                form.AddField("itemid", si.itemId);

                using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/checkrewards.php", form))
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
                        //Debug.Log(www.downloadHandler.text);
                        Debug.Log(si.itemId + " " + text);

                        if (text != null)
                        {
                            if (text == "exists")
                            {
                                // si.alreadyBought = true;
                                // Debug.Log(si.itemName + "-" +si.alreadyBought);                    
                                

                            }

                            else 
                            {
                                GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

                                //access button function
                                itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonPress(si));

                                //change sprite alpha
                                itemObject.transform.GetChild(0).GetComponent<Image>().sprite = si.itemImage;
                            }

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
       // PopulateShop();
        
    }


    /*  public void PopulateShop()
      {

          for (int i = 0; i < shopItemA.Length; i++)
          {
              ShopItemScript si = shopItemA[i]; //shop item from array


                  GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

                  //access button function
                  itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonPress(si));

                  //change sprite alpha
                  itemObject.transform.GetChild(0).GetComponent<Image>().sprite = si.itemImage;
                  Debug.Log(si.alreadyBought);


              //change item name



          }

      }*/

    private void Update()
    {
        shopText.text = "You have " + DBManager.userCurrency + " points";
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
               // Debug.Log(www.downloadHandler.text);

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

    private void OnButtonPress(ShopItemScript item)
    {
        openBuyPanel(item.itemName, item.itemCost, item.itemImage);
        disItemName = item.itemName;
        disItemID = item.itemId;
        disItemCost = item.itemCost;
    }

    public void openBuyPanel(string itemName, int itemCost, Sprite itemImage)
    {

       
        buyPanel.SetActive(true);
        itemNameText.text = itemName;
        itemPriceText.text = itemCost.ToString() + " Points" ;



    }

    public void closeBuyPanel()
    {

        buyPanel.SetActive(false);
    }

    public void purchaseItem()
    {
        var itemName = disItemName;
        var itemID = disItemID;
        var itemCost = disItemCost;

        StartCoroutine(validatePurchase(itemName, itemID, itemCost));
    }

    IEnumerator validatePurchase(string itemName, string itemID, int itemCost)
    {

        if (int.Parse(DBManager.userCurrency) >= itemCost)
        { 
        //scoring system
        WWWForm form = new WWWForm();
        form.AddField("userid", DBManager.userid);
        form.AddField("itemid", itemID);
        form.AddField("itemname", itemName);
        form.AddField("itemprice", itemCost);
                       
        using (UnityWebRequest www = UnityWebRequest.Post("http://learnplusultra.xyz/app_scripts/insertreward.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                SceneManager.LoadScene("Shop_Menu");
                Debug.Log("Error");
            }

            else
            {
                string text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);

                if (text != null)
                {
                        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        StartCoroutine(delayPurchase());
                }

                else
                {

                    Debug.Log("Try again");
                }

            }
        }
        }

        else
        {
            StartCoroutine(invalidPurchase());
        }
    }

    IEnumerator invalidPurchase()
    {
        yield return new WaitForSeconds(0.2f);
        buyButton.interactable = false;
        invalidGO.SetActive(true);
        invalidFX.Play();
        yield return new WaitForSeconds(1f);
        invalidGO.SetActive(false);
        buyButton.interactable = true;

    }

    IEnumerator delayPurchase()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
}
