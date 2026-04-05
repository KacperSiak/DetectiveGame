using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class JournalCheck : MonoBehaviour
{
    [SerializeField] private List<WordSlot> slots = new();
    [SerializeField] private Button journalCheck;

    private int goodSlotCounter;

    void Start()
    {
        journalCheck.onClick.AddListener(CheckJournal);
    }

    private void CheckJournal()
    {
        goodSlotCounter = 0;
        foreach (var slot in slots)
        {
            if(slot.transform.childCount >0)
            {
                DraggableWord word = slot.GetComponentInChildren<DraggableWord>();

                if ((word.ID == slot.slotID))
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
