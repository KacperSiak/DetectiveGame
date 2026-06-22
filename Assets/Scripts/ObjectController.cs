using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private string itemName;

    public void ShowObjectName()
    {
        InspectController.Instance.ShowName(itemName);

    }

    public void HideObjectName()
    {
        InspectController.Instance.HideName();
    }
    
}
