using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ClueEntry
{
    public string description;
    public Sprite photo;
}

public class JournalManager : MonoBehaviour
{
    public static JournalManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private Canvas mainUICanvas; // main hud canvas
    [SerializeField] private Transform clueContainer; //vertical layer group
    [SerializeField] private GameObject cluePrefab; // prefab with image and textmeshpro

    [Header("Notification")]
    [SerializeField] private GameObject newClueNotification;
    [SerializeField] private float popupDuration = 3f;

    [Header("Photo Settings")]
    [SerializeField] private int photoSize = 512; // size of photo

    private List<ClueEntry> discoveredClues = new List<ClueEntry>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        journalPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !InspectSystem.IsInspecting)
        {
            ToggleJournal();
        }
    }

    public void AddClue(string description)
    {
        // prevent duplicates
        foreach (var entry in discoveredClues)
        {
            if (entry.description == description) return;
        }

        StartCoroutine(CapturePhotoRoutine(description));
    }

    //take screenshot
    private IEnumerator CapturePhotoRoutine(string description)
    {
        //hide ui
        mainUICanvas.enabled = false;

        // wait for new frame
        yield return new WaitForEndOfFrame();

        // center position
        int centerX = (Screen.width - photoSize) / 2;
        int centerY = (Screen.height - photoSize) / 2;

        // create texture
        Texture2D screenshot = new Texture2D(photoSize, photoSize, TextureFormat.RGB24, false);

        // read only the pixels in the center square
        screenshot.ReadPixels(new Rect(centerX, centerY, photoSize, photoSize), 0, 0);
        screenshot.Apply();

        // show ui again
        mainUICanvas.enabled = true;

        // convert and save image
        Sprite photoSprite = Sprite.Create(screenshot, new Rect(0, 0, photoSize, photoSize), new Vector2(0.5f, 0.5f));

        ClueEntry newEntry = new ClueEntry { description = description, photo = photoSprite };
        discoveredClues.Add(newEntry);

        CreateJournalUIEntry(newEntry);
        StartCoroutine(ShowNotification());
    }

    private void CreateJournalUIEntry(ClueEntry entry)
    {
        GameObject newClueObj = Instantiate(cluePrefab, clueContainer);

        // create prefab with image and textmesh
        newClueObj.GetComponentInChildren<Image>().sprite = entry.photo;
        newClueObj.GetComponentInChildren<TextMeshProUGUI>().text = entry.description;
    }

    public void ToggleJournal()
    {
        bool isActive = !journalPanel.activeSelf;
        journalPanel.SetActive(isActive);

        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
        Time.timeScale = isActive ? 0 : 1;
    }

    private IEnumerator ShowNotification()
    {
        if (newClueNotification == null) yield break;
        newClueNotification.SetActive(true);
        yield return new WaitForSecondsRealtime(popupDuration);
        newClueNotification.SetActive(false);
    }
}