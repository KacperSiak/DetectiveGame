using UnityEngine;

public enum CluesCheckerSection
{
    Tool,
    Suspects,
    Evidence
}
public class InspectClue : MonoBehaviour
{
    [SerializeField] public string clueDescription;
    [SerializeField] private string clueText;
    [SerializeField] private int clueID;
    [SerializeField] private string clueNameForCheckingManager;
    [SerializeField] private CluesCheckerSection clueSection; // section of clues in journal clues checker
    [SerializeField] private JournalTextPage myRoom;

    public void OnFound()
    {
        Debug.Log("Clue: " + clueDescription);
        // do stuff when clue found xd
        JournalManager.Instance.AddClue(clueDescription); // the stuff when clue found xd

        JournalEvents.ClueFound(clueText, clueNameForCheckingManager, clueID, clueSection, myRoom);

        if(TryGetComponent<VoiceLineProc>(out var VLP))
        {
            VLP.ProcVoiceLine();
        }

        if (TryGetComponent<SceneUnlocker>(out var SU))
        {
            SU.UnlockScene();
        }
    }
}