using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private string itemName;

    [SerializeField] private InspectController inspectController;

    public void ShowObjectName()
    {
        inspectController.ShowName(itemName);

    }

    public void HideObjectName()
    {
        inspectController.HideName();
    }
    
}
