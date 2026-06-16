using UnityEngine;

public class PadLockController : MonoBehaviour
{
    [SerializeField] private int[] correctCode;

    private bool[] correctPlaces;

    private void Start()
    { 
        correctPlaces = new bool[correctCode.Length];
    }
    public void GetNewNumber(int place, int digit)
    {
        if (correctCode[place] == digit)
        {
            correctPlaces[place] = true;
            CheckCode();
        }
        else correctPlaces[place] = false;
    }

    private void CheckCode()
    {
        int correctCounter = 0;
        foreach(var place in correctPlaces)
        {
            if (place) correctCounter++;
        }

        if (correctCounter == correctPlaces.Length) Debug.Log("K³ódka otwarta");
    }
}
