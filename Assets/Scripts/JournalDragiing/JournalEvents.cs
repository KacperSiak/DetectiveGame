using UnityEngine;
using System;

public class JournalEvents : MonoBehaviour
{
    public static Action<string, int> OnClueFind;

    public static void ClueFound(string name, int wordID)
    {
        OnClueFind?.Invoke(name, wordID);
    }
}
