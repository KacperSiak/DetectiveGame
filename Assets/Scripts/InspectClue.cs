using UnityEngine;

public enum CluesCheckerSection
{
    Tool,
    Suspects
}
public class InspectClue : MonoBehaviour
{
    [SerializeField] public string clueDescription;
    [SerializeField] private string clueText;
    [SerializeField] private int clueID;
    [SerializeField] private CluesCheckerSection clueSection; // section of clues in journal clues checker

    public void OnFound()
    {
        Debug.Log("Clue: " + clueDescription);
        // do stuff when clue found xd
        JournalManager.Instance.AddClue(clueDescription); // the stuff when clue found xd

        JournalEvents.ClueFound(clueText, clueID, clueSection);
    }
}