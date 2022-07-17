using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Private Variable
    [SerializeField] private uint npcID;
    [SerializeField] private string npcName;

    //Private Function
    public virtual void OnEnable()
    {
        if (!NPCManager.Instance.NpcDictionary.ContainsKey(NpcID))
        {
            NPCManager.Instance.NpcDictionary.Add(NpcID, this);
        }
    }

    public virtual void OnDisable()
    {
        if (NPCManager.Instance.NpcDictionary.ContainsKey(NpcID))
        {
            NPCManager.Instance.NpcDictionary.Remove(NpcID);
        }
    }

    #region Property
    public uint NpcID
    {
        get { return npcID; }
        private set { npcID = value; }
    }
    public string NpcName
    {
        get { return npcName; }
        set { npcName = value; }
    }
    #endregion
}
