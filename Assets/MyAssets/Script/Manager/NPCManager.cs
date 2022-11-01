using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum NPC_ID
{
    LEADER_NPC = 1000,
    STORE_NPC = 2000,
    FORGE_NPC = 3000
}

public class NPCManager
{
    private Dictionary<uint, NPC> npcDictionary;

    public void Initialize()
    {
        npcDictionary = new Dictionary<uint, NPC>();
    }

    #region Property
    public Dictionary<uint, NPC> NpcDictionary
    {
        get { return npcDictionary; }
        set { npcDictionary = value; }
    }
    #endregion
}
