using UnityEngine;
using TMPro;

public class StatusPopUp : PopUp
{
    [SerializeField] private TextMeshProUGUI classText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hitPointText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI attackPowerText;
    [SerializeField] private TextMeshProUGUI defensivePowerText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;
    [SerializeField] private TextMeshProUGUI criticalChanceText;
    [SerializeField] private TextMeshProUGUI criticalDamageText;
    
    [SerializeField] private TextMeshProUGUI statPointText;
    
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI vitalityText;
    [SerializeField] private TextMeshProUGUI dexterityText;
    [SerializeField] private TextMeshProUGUI luckText;

    #region Button Event Function
    public void IncreaseStrength()
    {
        if (Managers.GameManager.CurrentCharacter.CharacterData.StatPoint > 0)
        {
            --Managers.GameManager.CurrentCharacter.CharacterData.StatPoint;
            ++Managers.GameManager.CurrentCharacter.CharacterData.Strength;
        }
    }
    public void IncreaseVitality()
    {
        if (Managers.GameManager.CurrentCharacter.CharacterData.StatPoint > 0)
        {
            --Managers.GameManager.CurrentCharacter.CharacterData.StatPoint;
            ++Managers.GameManager.CurrentCharacter.CharacterData.Vitality;
        }
    }

    public void IncreaseDexterity()
    {
        if (Managers.GameManager.CurrentCharacter.CharacterData.StatPoint > 0)
        {
            --Managers.GameManager.CurrentCharacter.CharacterData.StatPoint;
            ++Managers.GameManager.CurrentCharacter.CharacterData.Dexterity;
        }
    }

    public void IncreaseLuck()
    {
        if (Managers.GameManager.CurrentCharacter.CharacterData.StatPoint > 0)
        {
            --Managers.GameManager.CurrentCharacter.CharacterData.StatPoint;
            ++Managers.GameManager.CurrentCharacter.CharacterData.Luck;
        }
    }
    #endregion


    #region Property
    public TextMeshProUGUI ClassText
    {
        get { return classText; }
        private set { classText = value; }
    }
    public TextMeshProUGUI LevelText
    {
        get { return levelText; }
        private set { levelText = value; }
    }
    public TextMeshProUGUI HitPointText
    {
        get { return hitPointText; }
        private set { hitPointText = value; }
    }
    public TextMeshProUGUI StaminaText
    {
        get { return staminaText; }
        private set { staminaText = value; }
    }
    public TextMeshProUGUI AttackPowerText
    {
        get { return attackPowerText; }
        private set { attackPowerText = value; }
    }
    public TextMeshProUGUI DefensivePowerText
    {
        get { return defensivePowerText; }
        private set { defensivePowerText = value; }
    }
    public TextMeshProUGUI AttackSpeedText
    {
        get { return attackSpeedText; }
        private set { attackSpeedText = value; }
    }
    public TextMeshProUGUI MoveSpeedText
    {
        get { return moveSpeedText; }
        private set { moveSpeedText = value; }
    }
    public TextMeshProUGUI CriticalChanceText
    {
        get { return criticalChanceText; }
        private set { criticalChanceText = value; }
    }
    public TextMeshProUGUI CriticalDamageText
    {
        get { return criticalDamageText; }
        private set { criticalDamageText = value; }
    }

    public TextMeshProUGUI StatPointText
    {
        get { return statPointText; }
        private set { statPointText = value; }
    }

    public TextMeshProUGUI StrengthText
    {
        get { return strengthText; }
        private set { strengthText = value; }
    }
    public TextMeshProUGUI VitalityText
    {
        get { return vitalityText; }
        private set { vitalityText = value; }
    }
    public TextMeshProUGUI DexterityText
    {
        get { return dexterityText; }
        private set { dexterityText = value; }
    }
    public TextMeshProUGUI LuckText
    {
        get { return luckText; }
        private set { luckText = value; }
    }
    #endregion
}
