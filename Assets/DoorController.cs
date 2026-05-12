using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    bool _IsDoorOpen = false;
    [SerializeField] int doorSide;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InteractWithDoor()
    {
        if(_IsDoorOpen) CloseDoor();
        else OpenDoor();
    }
    private void OpenDoor()
    {
        transform.DORotate(new Vector3(0, doorSide, 0), 1f, RotateMode.Fast);
        _IsDoorOpen = true;
    }

    private void CloseDoor()
    {
        transform.DORotate(Vector3.zero, 1f, RotateMode.Fast);
        _IsDoorOpen = false;
    }
}
