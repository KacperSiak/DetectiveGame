using UnityEngine;

public class WordBankAdd : MonoBehaviour
{
    [SerializeField] private GameObject prefab; 
    void Start()
    {
        JournalEvents.OnClueFind += AddWord;
    }

    private void AddWord(string wordName, int wordID)
    {
        GameObject word = Instantiate(prefab, transform);
        DraggableWord wordScript = word.GetComponent<DraggableWord>();
        wordScript.Init(wordID, wordName);
    }
}
