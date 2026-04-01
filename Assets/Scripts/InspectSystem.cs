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

    [Header("Click vs Drag Settings")]
    [SerializeField] private float clickTimeThreshold = 0.2f; // max time for a 'click'
    [SerializeField] private float dragThreshold = 10f; // pixels moved before it counts as a drag

    private float mouseClickStartTime;
    private Vector2 mouseClickStartPosition;
    private bool isDragging;

    //private bool isMoving = false;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoomDist = 0.5f; // closest point to camera
    [SerializeField] private float maxZoomDist = 2.5f; // furthest point from camera
    private float currentZoomDist;

    void Update()
    {
        if (currentItem == null) return;

        HandleZoom();

        // move the item to the socket in front of camera
        Vector3 targetPos = Camera.main.transform.position + (Camera.main.transform.forward * currentZoomDist);
        currentItem.position = Vector3.Lerp(currentItem.position, targetPos, Time.deltaTime * moveSpeed);

        HandleInput();

        // right click to stop inspecting
        if (Input.GetMouseButtonDown(1))
        {
            StopInspecting();
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // Adjust distance and clamp it so the item doesn't fly into the player's brain
            currentZoomDist += -scroll * zoomSpeed;
            currentZoomDist = Mathf.Clamp(currentZoomDist, minZoomDist, maxZoomDist);
        }
    }
    private void HandleInput()
    {
        // when pressed
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickStartTime = Time.time;
            mouseClickStartPosition = Input.mousePosition;
            isDragging = false;
        }

        // while held
        if (Input.GetMouseButton(0))
        {
            float moveDistance = Vector2.Distance(Input.mousePosition, mouseClickStartPosition);

            // move mouse == drag
            if (moveDistance > dragThreshold)
            {
                isDragging = true;
                RotateItem();
            }
        }

        // release
        if (Input.GetMouseButtonUp(0))
        {
            float clickDuration = Time.time - mouseClickStartTime;

            // short press && no drag == click
            if (clickDuration < clickTimeThreshold && !isDragging)
            {
                CheckForClue();
            }
        }
    }

    private void RotateItem()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float rotY = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        currentItem.Rotate(inspectionSocket.up, rotY, Space.World);
        currentItem.Rotate(inspectionSocket.right, rotX, Space.World);
    }
    private void CheckForClue()
    {
        // shoot a ray from the center of the camera forward
        //Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        //shoot a ray from the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

        // start item at socket distance
        currentZoomDist = Vector3.Distance(Camera.main.transform.position, inspectionSocket.position);

        // disable physics
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}