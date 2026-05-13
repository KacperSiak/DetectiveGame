using UnityEngine;

public class WordBankAdd : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        JournalEvents.OnClueFind += AddWord;
    }
    void Start()
    {
        
    }

    private void AddWord(string wordName, int wordID)
    {
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
