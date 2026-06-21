using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceLineProc : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI voiceLineBar;
    [SerializeField] [TextArea] private string voiceLine;
    public void ProcVoiceLine()
    {
        voiceLineBar.gameObject.SetActive(true);
        voiceLineBar.text = voiceLine;
        StartCoroutine(HideTextAfterDelay(10));
    }

    IEnumerator HideTextAfterDelay(int seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        voiceLineBar.gameObject.SetActive(false);
    }
}
