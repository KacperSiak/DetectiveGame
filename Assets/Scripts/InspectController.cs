using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InspectController : MonoBehaviour
{
    public static InspectController Instance;

    [SerializeField] private GameObject objectNameBG;
    [SerializeField] private TextMeshProUGUI objectNameUI;
    [SerializeField] private TextMeshProUGUI voiceLineBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        objectNameBG.SetActive(false);
    }
    void Update()
    {
        
    }

    public void ShowName(string objectName)
    {
        objectNameBG.SetActive(true);
        objectNameUI.text= objectName;
    }

    public void HideName()
    {
        objectNameBG.SetActive(false);
        objectNameUI.text= string.Empty;
    }

    public void ProcVoiceLine(string voiceLine)
    {
        voiceLineBar.gameObject.SetActive(true);
        voiceLineBar.text = voiceLine;
        StartCoroutine(HideTextAfterDelay(5));
    }

    IEnumerator HideTextAfterDelay(int seconds)
    {

        yield return new WaitForSeconds(seconds);
        voiceLineBar.gameObject.SetActive(false);
    }

}
