using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UIElements;

public class PadLockCameraFocus : MonoBehaviour
{
    [SerializeField] private GameObject cameraPlace;

    private InteractionRaycast _ir;
    private bool isFocusing = false;
    private GameObject objectCamera;

    private void Update()
    {
        if (!isFocusing) return;

        if (Input.GetMouseButtonDown(1)) ExitFocuseMode();
    }

    public void EnterFocusMode(GameObject mainCamera, InteractionRaycast ir)
    {
        isFocusing = true;
        objectCamera = mainCamera;
        mainCamera.transform.SetParent(cameraPlace.transform, false);
        mainCamera.transform.DOLocalMove(Vector3.zero, 1f);
        _ir = ir;
    }

    private void ExitFocuseMode()
    {
        _ir.GoBack(objectCamera);
    }

}
