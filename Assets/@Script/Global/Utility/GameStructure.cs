using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlot
{
    public int slotIndex;
    public SelectionCharacter selectionCharacter;
    public Vector3 characterPoint;
    public TextMeshProUGUI slotText;
    public Button slotButton;

    public CharacterSlot()
    {
        slotText = null;
        slotButton = null;
    }
}

[System.Serializable]
public class ObjectPool
{
    public string key;
    public GameObject value;
    public int amount;
    public Queue<GameObject> queue = new Queue<GameObject>();

    public void Initialize(GameObject parent)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject poolObject = GameObject.Instantiate(value, parent.transform);
            poolObject.SetActive(false);
            queue.Enqueue(poolObject);
        }
    }
}

public enum SCENE_LIST
{
    // Common
    Title,
    Selection,
    Loading,

    // Chapter 01
    Forestia,
    Starlight_Forest,
    Dragon_Temple

    //
}

public enum SCENE_TYPE
{
    UNKNOWN,
    TITLE,
    SELECTION,
    VILIAGE,
    DUNGEON,
    LOADING,

    LENGTH
}

public enum CHARACTER_CLASS
{
    Null,
    Lancer
}

public enum CHARACTER_STATE
{
    // Common Character
    MOVE = 1,
    ATTACK = 2,
    SKILL = 4,
    ROLL = 5,
    HIT = 6,
    HEAVY_HIT = 7,
    STUN = 8,
    COMPETE = 9,
    DIE = 10,

    // Lancer
    LANCER_DEFENSE = 100

}

public enum CHARACTER_STATE_WEIGHT
{
    // Common Character
    MOVE = 1,
    ATTACK = 2,
    SKILL = 3,
    ROLL = 4,
    HIT = 5,
    HEAVY_HIT = 6,
    STUN = 7,
    COMPETE = 8,
    DIE = 9,

    // Lancer
    LANCER_DEFENSE = 2
}

public enum ATTACK_TYPE
{
    COMBO1,
    COMBO2,
    COMBO3,
    COMBO4,

    SMASH1,
    SMASH2,
    SMASH3,
    SMASH4,

    SKILL
}

public enum CURSOR_MODE
{
    LOCK,
    UNLOCK
}

public enum UI_EVENT
{
    CLICK,
    PRESS
}