using UnityEngine.Events;

public class TitlePanel : Panel
{
    public static event UnityAction onToggleHelpPopUp;
    public void ToggleHelpPopUp()
    {
        onToggleHelpPopUp();
    }
}
