using UnityEngine;

public class InspectSystem : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform inspectionSocket; // socket (empty) in front of camera
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float moveSpeed = 10f;

    private Transform currentItem;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isMoving = false;

    void Update()
    {
        if (currentItem == null) return;

        // move the item to the socket in front of camera
        if (Vector3.Distance(currentItem.position, inspectionSocket.position) > 0.01f)
        {
            currentItem.position = Vector3.Lerp(currentItem.position, inspectionSocket.position, Time.deltaTime * moveSpeed);
        }

        // rotate using left click
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float rotY = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            currentItem.Rotate(inspectionSocket.up, rotY, Space.World);
            currentItem.Rotate(inspectionSocket.right, rotX, Space.World);
        }

        // right click to stop inspecting
        if (Input.GetMouseButtonDown(1))
        {
            StopInspecting();
        }
    }

    public void StartInspecting(Transform itemTransform)
    {
        if (currentItem != null) return; // check if already inspecting

        currentItem = itemTransform;

        // save original position
        originalPos = currentItem.position;
        originalRot = currentItem.rotation;

        // disable physics
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    public void StopInspecting()
    {
        // return item
        currentItem.position = originalPos;
        currentItem.rotation = originalRot;

        // Re-enable physics if needed
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        currentItem = null;
    }
}