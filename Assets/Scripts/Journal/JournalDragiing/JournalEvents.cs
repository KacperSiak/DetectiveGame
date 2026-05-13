using UnityEngine;
using System;

public class JournalEvents : MonoBehaviour
{
    public static Action<string, int> OnClueFind;
    public static Action<string, int, CluesCheckerSection> OnClueForCheckerFound;

    public static void ClueFound(string nameForDragling, string nameForChecking, int wordID, CluesCheckerSection section)
    {
        OnClueFind?.Invoke(nameForDragling, wordID);
        OnClueForCheckerFound?.Invoke(nameForChecking, wordID, section);
    }
}
