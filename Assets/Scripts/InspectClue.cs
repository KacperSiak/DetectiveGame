using UnityEngine;

public class InspectClue : MonoBehaviour
{
    [SerializeField] public string clueDescription;

    public void OnFound()
    {
        Debug.Log("Clue: " + clueDescription);
        // do stuff when clue found xd
        JournalManager.Instance.AddClue(clueDescription); // the stuff when clue found xd
    }
}