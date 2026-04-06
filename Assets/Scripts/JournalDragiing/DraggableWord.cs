using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int itemID;// { private set; get; }
    [SerializeField] private TextMeshProUGUI itemName;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Transform _startParent;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(int ID, string name)
    {
        itemID = ID;
        itemName.text = name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;

        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _startParent)
        {
            transform.position = _startPosition;
        }
    }
}
