using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HuntTask : QuestTask
{
    [SerializeField] private string targetName;

    public override void StartTask()
    {
        onTaskStart(this);
        Monster.onDie -= Action;
        Monster.onDie += Action;
    }

    public override void EndTask()
    {
        Monster.onDie -= Action;
        ++OwnerQuest.TaskIndex;
        onTaskEnd(this);
    }

    public void Action(Monster monster)
    {
        if(monster.MonsterName == targetName)
        {
            ++SuccessAmount;
            UIManager.Instance.RequestNotice(targetName + " 처치: " + SuccessAmount + "/" + RequireAmount);
        }
    }
    #region Property
    public string TargetName
    {
        get { return targetName; }
    }
    #endregion
}
