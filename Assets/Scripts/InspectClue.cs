using UnityEngine;

public class InspectClue : MonoBehaviour
{
    [SerializeField] public string clueDescription;
    [SerializeField] private string clueText;
    [SerializeField] private int clueID;

    public void OnFound()
    {
        Debug.Log("Clue: " + clueDescription);
        // do stuff when clue found xd
        JournalManager.Instance.AddClue(clueDescription); // the stuff when clue found xd

        JournalEvents.ClueFound(clueText, clueID);
    }
}