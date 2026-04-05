using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    private float _xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (InspectSystem.IsInspecting) return; //if inspecting an item, stop camera movement

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler( _xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if ((Input.GetKeyDown(KeyCode.E) && Cursor.lockState == CursorLockMode.None))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
