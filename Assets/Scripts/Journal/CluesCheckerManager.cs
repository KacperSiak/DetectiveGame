using System;
using UnityEngine;

public class CluesCheckerManager : MonoBehaviour
{
    [SerializeField] private Material matForSuspects;
    [SerializeField] private Material matForTools;
    [SerializeField] private GameObject wordPrefab;

    private void Awake()
    {
        JournalEvents.OnClueForCheckerFound += AddClueForChecker;
    }

    private void AddClueForChecker(string name, int ID, CluesCheckerSection section)
    {

        foreach (Transform child in transform)
        {
            child.TryGetComponent<CluesStateJournalCheckerWord>(out var foundWord);
            if (foundWord.localObjectName == name) return;
        }

        var newWord = Instantiate(wordPrefab, transform);
        CluesStateJournalCheckerWord word = newWord.GetComponent<CluesStateJournalCheckerWord>();

        switch (section)
        {
            case CluesCheckerSection.Tool:
                word.Init(name, matForTools.color);
                break;

            case CluesCheckerSection.Suspects:
                word.Init(name, matForSuspects.color);
                break;
        }
    }

    private void OnDestroy()
    {
        JournalEvents.OnClueForCheckerFound -= AddClueForChecker;
    }
}
