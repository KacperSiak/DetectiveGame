using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalManager : MonoBehaviour
{
    public static JournalManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private TextMeshProUGUI clueListText; // A single large text block or a template
    [SerializeField] private GameObject newClueNotification; // new clue popup
    [SerializeField] private float popupDuration = 3f;

    private List<string> discoveredClues = new List<string>();

    private void Awake()
    {
        // Singleton setup
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        journalPanel.SetActive(false);
        if (newClueNotification) newClueNotification.SetActive(false);
    }

    private void Update()
    {
        // Toggle Journal with 'J' key
        if (Input.GetKeyDown(KeyCode.J) && !InspectSystem.IsInspecting)
        {
            ToggleJournal();
        }
    }

    public void AddClue(string description)
    {
        // Don't add the same clue twice
        if (!discoveredClues.Contains(description))
        {
            discoveredClues.Add(description);
            UpdateJournalUI();
            StartCoroutine(ShowNotification());
            //Debug.Log("Journal Updated: " + description);
        }
    }

    private void UpdateJournalUI()
    {
        clueListText.text = ""; // Clear current text
        foreach (string clue in discoveredClues)
        {
            clueListText.text += "• " + clue + "\n\n";
        }
    }

    public void ToggleJournal()
    {
        bool isActive = !journalPanel.activeSelf;
        journalPanel.SetActive(isActive);

        // Handle cursor and movement when journal is open
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0; // Optional: Pause game while reading
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    private System.Collections.IEnumerator ShowNotification()
    {
        if (newClueNotification == null) yield break;

        newClueNotification.SetActive(true);
        yield return new WaitForSeconds(popupDuration);
        newClueNotification.SetActive(false);
    }
}