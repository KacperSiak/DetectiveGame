using UnityEngine;

public enum JournalTextPage
{
    Eleanor,
    Pharmacy,
    Workshop,
    Residence
}

public class WordBankAdd : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private JournalTextPage page;

    private void Awake()
    {
        JournalEvents.OnClueFind += AddWord;
    }
    void Start()
    {
        
    }

    private void AddWord(string wordName, int wordID, JournalTextPage clueRoom)
    {
        if (clueRoom != page) return;

        foreach(Transform child in transform)
        {
            child.TryGetComponent<DraggableWord>(out var foundWord);
            if (foundWord.localItemName == wordName) return;
        }

        GameObject word = Instantiate(prefab, transform);
        DraggableWord wordScript = word.GetComponent<DraggableWord>();
        wordScript.Init(wordID, wordName);
    }

    private void OnDestroy()
    {
        JournalEvents.OnClueFind -= AddWord;
    }
}
