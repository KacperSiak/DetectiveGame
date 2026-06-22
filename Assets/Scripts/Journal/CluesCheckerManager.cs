using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CluesCheckerManager : MonoBehaviour
{
    [SerializeField] private Material matForSuspects;
    [SerializeField] private Material matForTools;
    [SerializeField] private Material matForEvidences;
    [SerializeField] private GameObject wordPrefab;

    [SerializeField] private List<GameObject> rowList;
    private Dictionary<GameObject, int> rowsAndLenght = new Dictionary<GameObject, int>();
    private int digitInRow = 60;

    private void Awake()
    {
        JournalEvents.OnClueForCheckerFound += AddClueForChecker;
    }

    private void Start()
    {
        foreach (var row in rowList)
        {
            rowsAndLenght.Add(row, digitInRow);
        }
    }

    private void AddClueForChecker(string name, CluesCheckerSection section)
    {
        if (name == null) return;

        foreach (Transform child in transform)
        {
            bool found = SearchInRowChildren(child, name);
            if (found) return;
        }

        Transform wordParent = DecideTheParent(name);

        var newWord = Instantiate(wordPrefab, wordParent);
        CluesStateJournalCheckerWord word = newWord.GetComponent<CluesStateJournalCheckerWord>();

        switch (section)
        {
            case CluesCheckerSection.Tool:
                word.Init(name, matForTools.color);
                break;

            case CluesCheckerSection.Suspects:
                word.Init(name, matForSuspects.color);
                break;

            case CluesCheckerSection.Evidence:
                word.Init(name, matForEvidences.color);
                break;
        }
    }

    private Transform DecideTheParent(string name)
    {
        int wordLenght = name.Length;

        foreach(var row in rowsAndLenght)
        {
            if(row.Value >= wordLenght)
            {
                var key = row.Key;
                rowsAndLenght[key] -= wordLenght;

                return key.transform;
            }
        }
        
        return null;
    }

    private bool SearchInRowChildren(Transform parent, string targetName)
    {
        foreach(Transform child in parent)
        {
            if (child.TryGetComponent<CluesStateJournalCheckerWord>(out var foundWord))
            { 
                if (foundWord.localObjectName == targetName) return true; 
            }
        }

        return false;
    }

    private void OnDestroy()
    {
        JournalEvents.OnClueForCheckerFound -= AddClueForChecker;
    }
}
