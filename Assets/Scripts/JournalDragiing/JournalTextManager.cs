using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class JournalTextManager : MonoBehaviour
{
    [SerializeField] private List<WordSlot> slots = new();
    [SerializeField] private Button journalCheck;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject wordBank;

    private int goodSlotCounter;

    void Start()
    {
        journalCheck.onClick.AddListener(CheckJournal);
        journal.SetActive(false);
        wordBank.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            journal.SetActive(true);
            wordBank.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.None))
        {
            Cursor.lockState = CursorLockMode.Locked;
            journal.SetActive(false);
            wordBank.SetActive(false);
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
            Debug.Log("Wszystko dzia³a");
        }
        else
        {
            Debug.Log("Mamy b³ad");
        }
    }
}
