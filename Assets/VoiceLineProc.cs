using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceLineProc : MonoBehaviour
{
    [SerializeField] [TextArea] private string voiceLine;
    public void ProcVoiceLine()
    {
        InspectController.Instance.ProcVoiceLine(voiceLine);
    }


}
