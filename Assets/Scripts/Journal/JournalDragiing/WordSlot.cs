using UnityEngine;
using UnityEngine.EventSystems;

public class WordSlot : MonoBehaviour, IDropHandler 
{
    public int slotID;
    [SerializeField] private Transform wordBank;
    private bool _isTaken = false;


    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) _isTaken = false;

        if (eventData.pointerDrag != null && !_isTaken)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.transform.SetParent(transform);
            _isTaken = true;
        }
        else if (eventData.pointerDrag != null && _isTaken)
        {  
            Transform childTransform = transform.GetChild(0);
            GameObject currentChild = childTransform.gameObject;
            currentChild.transform.SetParent(wordBank, false);
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.transform.SetParent(transform);
            _isTaken = true;
        }
    }
}
