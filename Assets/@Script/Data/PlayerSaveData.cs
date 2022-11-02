using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public string playerClass;
    public int level;
    public float currentExperience;
    public float maxExperience;
    public int statPoint;
    public int strength;
    public int vitality;
    public int dexterity;
    public int luck;
    public uint mainQuestProcedure;
    public int money;

    public string[] inventoryItemNames;
    public int[] inventoryItemCounts;

    public string[] quickSlotItemNames;
    public int[] quickSlotItemCounts;

    public string[] equipmentSlotItemNames;
    public int[] equipmentSlotItemCounts;

    public List<QuestSaveData> questSaveList;
}
