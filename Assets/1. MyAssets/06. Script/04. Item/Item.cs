using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ITEM_TYPE
{
    EMPTY = 0,

    WEAPON = 1,
    HELMET = 2,
    ARMOR = 3,
    BOOTS = 4,

    CONSUMPTION = 200,
    OTHER = 300,
    QUEST = 400
}
public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private ITEM_TYPE itemType;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int itemPrice;
    [SerializeField] private bool isCountable;

    #region Property
    public string ItemName
    {
        get { return itemName; }
        private set { itemName = value; }
    }
    public ITEM_TYPE ItemType
    {
        get { return itemType; }
        private set { itemType = value; }
    }
    public Sprite ItemSprite
    {
        get { return itemSprite; }
        private set { itemSprite = value; }
    }
    public GameObject ItemPrefab
    {
        get { return itemPrefab; }
        private set { itemPrefab = value; }
    }
    public int ItemPrice
    {
        get { return itemPrice; }
        private set { itemPrice = value; }
    }
    public bool IsCountable
    {
        get { return isCountable; }
    }
    #endregion
}
