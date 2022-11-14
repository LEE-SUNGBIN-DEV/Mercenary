using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserPanel : UIPanel
{
    [SerializeField] private Image hitPointBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image experienceBar;

    public override void Initialize()
    {
        Managers.DataManager.CurrentCharacter.CharacterData.OnPlayerDataChanged += (CharacterData playerData) =>
        {
            float expRatio = playerData.CurrentExperience / playerData.MaxExperience;
            SetUserExpBar(expRatio);
        };

        CharacterStats.OnCharacterStatsChanged += (CharacterStats characterStats) =>
        {
            float ratio = characterStats.CurrentHitPoint / characterStats.MaxHitPoint;
            SetUserHPBar(ratio);

            ratio = characterStats.CurrentStamina / characterStats.MaxStamina;
            SetUserStaminaBar(ratio);
        };
    }

    public void SetUserHPBar(float ratio)
    {
        hitPointBar.fillAmount = ratio;
    }

    public void SetUserStaminaBar(float ratio)
    {
        staminaBar.fillAmount = ratio;
    }

    public void SetUserExpBar(float ratio)
    {
        experienceBar.fillAmount = ratio;
    }
}
