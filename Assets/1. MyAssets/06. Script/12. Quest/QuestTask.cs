using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class QuestTask
{
    #region Action
    public static UnityAction<QuestTask> onTaskStart;
    public static UnityAction<QuestTask> onTaskEnd;
    #endregion

    [SerializeField] private Quest ownerQuest;
    [TextArea]
    [SerializeField] private string taskDescription;

    [SerializeField] private int requireAmount;
    [SerializeField] private int successAmount;

    public abstract void StartTask();
    public abstract void EndTask();

    #region Property
    public Quest OwnerQuest
    {
        get { return ownerQuest; }
        set { ownerQuest = value; }
    }
    public string TaskDescription
    {
        get { return taskDescription; }
        private set { taskDescription = value; }
    }
    public int RequireAmount
    {
        get { return requireAmount; }
        private set { requireAmount = value; }
    }

    public int SuccessAmount
    {
        get { return successAmount; }
        set
        {
            successAmount = value;

            if (successAmount >= RequireAmount)
            {
                successAmount = RequireAmount;
                EndTask();
            }
        }
    }
    #endregion
}
