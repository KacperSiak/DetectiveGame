using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalEntryDisplay : MonoBehaviour
{
    // These slots let us tell the script EXACTLY which UI element is which
    public Image photoDisplay;
    public TextMeshProUGUI descriptionText;

    public void Setup(string text, Sprite photo)
    {
        descriptionText.text = text;
        photoDisplay.sprite = photo;
    }
}