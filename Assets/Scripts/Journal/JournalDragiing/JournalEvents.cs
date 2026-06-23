using UnityEngine;
using System;

public class JournalEvents : MonoBehaviour
{
    public static Action<string, int, JournalTextPage> OnClueFind;
    public static Action<string, CluesCheckerSection> OnClueForCheckerFound;

    public static void ClueFound(string nameForDragling, string nameForChecking, int wordID, CluesCheckerSection section, JournalTextPage page)
    {
        OnClueFind?.Invoke(nameForDragling, wordID, page);
        OnClueForCheckerFound?.Invoke(nameForChecking, section);
    }
}
