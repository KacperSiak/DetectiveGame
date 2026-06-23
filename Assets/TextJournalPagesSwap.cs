using UnityEngine;
using System.Collections.Generic;

public class TextJournalPagesSwap : MonoBehaviour
{
    [SerializeField] private List<GameObject> journalPages;
    private int _currentPage = 0;

    private void Start()
    {
        SetPage(0);
    }

    public void ChangePage(int side)
    {
        int _newPage = _currentPage + side;

        if (_newPage >= journalPages.Count) _newPage = 0;
        else if (_newPage < 0) _newPage = journalPages.Count - 1;

        SetPage(_newPage);
    }

    public void SetPage(int page)
    {

        _currentPage = Mathf.Clamp(page, 0, journalPages.Count - 1);

        for (int i = 0; i < journalPages.Count; i++)
        {
            journalPages[i].SetActive(page == i);
        }
    }
}
