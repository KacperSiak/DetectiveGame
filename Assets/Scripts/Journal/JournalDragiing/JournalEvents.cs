using UnityEngine;
using System;

public class JournalEvents : MonoBehaviour
{
    public static Action<string, int> OnClueFind;
    public static Action<string, int, CluesCheckerSection> OnClueForCheckerFound;

    public static void ClueFound(string name, int wordID, CluesCheckerSection section)
    {
        OnClueFind?.Invoke(name, wordID);
        OnClueForCheckerFound?.Invoke(name, wordID, section);
    }
}
