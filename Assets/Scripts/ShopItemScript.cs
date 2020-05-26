using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//reference https://www.youtube.com/watch?v=Fc4h057uu9w&t=100s
[CreateAssetMenu(menuName = "Shop_Menu/Shop Item")]
public class ShopItemScript : ScriptableObject
{
    public string itemName;
    public string itemId;
    public int itemCost;
    public Sprite itemImage;
    //public bool alreadyBought = false;

    //public GameObject itemCont;

    /* public void awake()
     {
         Color c = itemCont.GetComponent<SpriteRenderer>().color;
         c.a = 0.2f;

     }
     */



}
