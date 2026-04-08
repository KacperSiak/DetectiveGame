using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

public class JournalTextManager : MonoBehaviour
{
    [SerializeField] private List<WordSlot> slots = new();
    [SerializeField] private Button journalCheck;
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject wordBank;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI caseText;

    private int goodSlotCounter;

    void Start()
    {
        journalCheck.onClick.AddListener(CheckJournal);
        okButton.onClick.AddListener(OkButton);
        journal.SetActive(false);
        wordBank.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !InspectSystem.IsInspecting)
        {
            if(!journal.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                journal.SetActive(true);
                wordBank.SetActive(true);
                canvas.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                journal.SetActive(false);
                wordBank.SetActive(false);
                canvas.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    private void CheckJournal()
    {
        goodSlotCounter = 0;
        foreach (var slot in slots)
        {
            if(slot.transform.childCount >0)
            {
                DraggableWord word = slot.GetComponentInChildren<DraggableWord>();

                if ((word.itemID == slot.slotID))
                {
                    goodSlotCounter++;
                }
            }
        }

        if(goodSlotCounter == slots.Count)
        {
            caseText.gameObject.SetActive(true);
            caseText.text = "Case Solved";
        }
        else
        {
            caseText.gameObject.SetActive(true);
            caseText.text = "There is mistake";
        }
    }

    private void OkButton()
    {
        caseText.gameObject.SetActive(false);
    }
}
