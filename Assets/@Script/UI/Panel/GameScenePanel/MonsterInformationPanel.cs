using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInformationPanel : UIPanel
{
    [SerializeField] private Image bossHPBar;

    private void Awake()
    {
        BossRoomController.onUpdateBossHPBar -= SetBossHPBar;
        BossRoomController.onUpdateBossHPBar += SetBossHPBar;
    }

    public void SetBossHPBar(float ratio)
    {
        bossHPBar.fillAmount = ratio;
    }
}
