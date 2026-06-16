using UnityEngine;
using TMPro;

public class CluesStateJournalCheckerWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordText;

    public string localObjectName;

    public void Init(string name, Color wordColor)
    {
        localObjectName = name;
        
        wordText.text = name;
        wordText.color = wordColor;
    }
}
