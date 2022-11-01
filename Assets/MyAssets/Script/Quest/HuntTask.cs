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
        Enemy.onDie -= Action;
        Enemy.onDie += Action;
    }

    public override void EndTask()
    {
        Enemy.onDie -= Action;
        ++OwnerQuest.TaskIndex;
        onTaskEnd(this);
    }

    public void Action(Enemy monster)
    {
        if(monster.MonsterName == targetName)
        {
            ++SuccessAmount;
            Managers.UIManager.RequestNotice(targetName + " 처치: " + SuccessAmount + "/" + RequireAmount);
        }
    }
    #region Property
    public string TargetName
    {
        get { return targetName; }
    }
    #endregion
}
