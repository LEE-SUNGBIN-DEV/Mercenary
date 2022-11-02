public interface ITalkable
{
    public bool IsTalk { get; set; }
    public void OnDialogue();
    public void OffTalk();
}
