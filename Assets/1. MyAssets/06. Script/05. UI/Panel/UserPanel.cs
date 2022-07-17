using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserPanel : Panel
{
    [SerializeField] private Image hitPointBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image experienceBar;

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
