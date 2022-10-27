using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRoomController : MonoBehaviour
{
    #region Event
    public static event UnityAction<float> onUpdateBossHPBar;
    #endregion

    [SerializeField] private SpawnData bossData;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private GameObject[] bossRoomBarriers;
    [SerializeField] private Portal portal;
    [SerializeField] private GameObject entranceEffect;
    private Collider entranceCollider;

    private void OnEnable()
    {
        entranceCollider = GetComponent<Collider>();

        Monster.onCurrentHitPointChanged -= UpdateBossHPBar;
        Monster.onCurrentHitPointChanged += UpdateBossHPBar;

        Monster.onDie -= CheckDungeonClear;
        Monster.onDie += CheckDungeonClear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            StartBattle();
            entranceEffect.gameObject.SetActive(false);
        }
    }

    public void StartBattle()
    {
        entranceCollider.enabled = false;

        if (bossRoomBarriers.Length > 0)
        {
            for(int i=0; i<bossRoomBarriers.Length; ++i)
            {
                bossRoomBarriers[i].SetActive(true);
            }
        }

        for (int i = 0; i < bossData.spawnAmount; ++i)
        {
            Monster monster = MonsterPoolManager.Instance.RequestObject(bossData.monster).GetComponent<Monster>();
            monster.MonsterNavMeshAgent.Warp(bossSpawnPoint.position);
            monster.Target = GameManager.Instance.Player.transform;

            UIManager.Instance.EntrancePanel.EntranceText.text = monster.GetComponent<Monster>().MonsterName;
            UIManager.Instance.OpenPanel(PANEL_TYPE.ENTRANCE);
        }

        UIManager.Instance.BossPanel.SetBossHPBar(1f);
        UIManager.Instance.OpenPanel(PANEL_TYPE.BOSS);
    }

    public void UpdateBossHPBar(Monster monster)
    {
        if (monster.MonsterPoolKey == bossData.monster)
        {
            float ratio = monster.CurrentHitPoint / monster.MaxHitPoint;
            if (onUpdateBossHPBar != null)
            {
                onUpdateBossHPBar(ratio);
            }
        }
    }

    public void CheckDungeonClear(Monster monster)
    {
        if(monster.MonsterPoolKey == bossData.monster)
        {
            StartCoroutine(DungeonClear());
        }
    }

    IEnumerator DungeonClear()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;

        Monster.onCurrentHitPointChanged -= UpdateBossHPBar;
        Monster.onDie -= CheckDungeonClear;

        UIManager.Instance.BossPanel.SetBossHPBar(1f);
        UIManager.Instance.ClosePanel(PANEL_TYPE.BOSS);
        UIManager.Instance.RequestNotice("던전 클리어");

        portal.gameObject.SetActive(true);
    }
}
