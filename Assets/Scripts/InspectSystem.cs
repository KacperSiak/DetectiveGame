using UnityEngine;

public class InspectSystem : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform inspectionSocket; // socket (empty) in front of camera
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private LayerMask clueLayer; // set to clue
    [SerializeField] private float clueRayDistance = 1f;
    public static bool IsInspecting { get; private set; }

    private Transform currentItem;
    private Vector3 originalPos;
    private Quaternion originalRot;
    //private bool isMoving = false;

    void Update()
    {
        if (currentItem == null) return;

        // move the item to the socket in front of camera
        if (Vector3.Distance(currentItem.position, inspectionSocket.position) > 0.01f)
        {
            currentItem.position = Vector3.Lerp(currentItem.position, inspectionSocket.position, Time.deltaTime * moveSpeed);
        }

        // rotate using left click
     //   if (Input.GetMouseButton(0))
     //   {
         float rotX = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
         float rotY = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

         currentItem.Rotate(inspectionSocket.up, rotY, Space.World);
         currentItem.Rotate(inspectionSocket.right, rotX, Space.World);
     //   }

        if (Input.GetMouseButtonDown(0))
        {
            CheckForClue();
        }

        // right click to stop inspecting
        if (Input.GetMouseButtonDown(1))
        {
            StopInspecting();
        }
    }

    private void CheckForClue()
    {
        // shoot a ray from the center of the camera forward
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, clueRayDistance, clueLayer))
        {
            if (hit.collider.TryGetComponent<InspectClue>(out InspectClue clue))
            {
                clue.OnFound();
            }
        }
    }

    public void StartInspecting(Transform itemTransform)
    {
        if (currentItem != null) return; // check if already inspecting
        
        IsInspecting = true; //block other inputs

        currentItem = itemTransform;

        // save original position
        originalPos = currentItem.position;
        originalRot = currentItem.rotation;

        // disable physics
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StopInspecting()
    {
        // return item
        currentItem.position = originalPos;
        currentItem.rotation = originalRot;

        // reenable physics if needed
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        IsInspecting = false; // allow other inputs
        currentItem = null;

    }
}