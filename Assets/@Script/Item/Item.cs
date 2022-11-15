using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    EMPTY = 0,

    WEAPON = 10,
    HELMET = 20,
    ARMOR = 30,
    BOOTS = 40,

    CONSUMPTION = 200,
    OTHER = 300,
    QUEST = 400
}
public class Item : MonoBehaviour
{
    private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private ITEM_TYPE itemType;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int itemPrice;
    [SerializeField] private bool isCountable;

    #region Property
    public int ItemID { get { return itemID; } }
    public string ItemName { get { return itemName; } }
    public ITEM_TYPE ItemType { get { return itemType; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public GameObject ItemPrefab { get { return itemPrefab; } }
    public int ItemPrice { get { return itemPrice; } }
    public bool IsCountable { get { return isCountable; } }
    #endregion
}
