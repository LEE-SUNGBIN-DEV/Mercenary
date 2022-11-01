using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CampaignPopUp : PopUp
{
    public static event UnityAction<string> OnClickCampaignButton;

    [SerializeField] private Button forestButton;
    [SerializeField] private Button templeButton;

    private void Awake()
    {
        CharacterData.onMainQuestProcedureChanged += (CharacterData playerData) =>
        {
            bool canEnable = playerData.MainQuestProcedure >= 1000 ? true : false;
            SetForestButton(canEnable);

            canEnable = playerData.MainQuestProcedure >= 2000 ? true : false;
            SetTempleButton(canEnable);
        };
    }

    public void CampaignButton(string sceneName)
    {
        Managers.AudioManager.PlaySFX("Button Click");
        OnClickCampaignButton(sceneName);
    }

    public void SetForestButton(bool isEnable)
    {
        forestButton.interactable = isEnable;
    }

    public void SetTempleButton(bool isEnable)
    {
        templeButton.interactable = isEnable;
    }

    public Button ForestButton
    {
        get { return forestButton; }
        private set { forestButton = value; }
    }
    public Button TempleButton
    {
        get { return templeButton; }
        private set { templeButton = value; }
    }
}