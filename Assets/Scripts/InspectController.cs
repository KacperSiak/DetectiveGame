using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectController : MonoBehaviour
{
    [SerializeField] private GameObject objectNameBG;
    [SerializeField] private TextMeshProUGUI objectNameUI;

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

}
