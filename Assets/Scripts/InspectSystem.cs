using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class InspectSystem : MonoBehaviour
{
    private enum InspectionState { None, Inspecting, Returning }
    private InspectionState currentState = InspectionState.None;

    [Header("UI Canvas refs")]
    [SerializeField] private CanvasGroup crosshairGroup;
    [SerializeField] private float fadeSpeed = 5f;
    private Coroutine fadeCoroutine;

    [Header("Visuals")]
    [SerializeField] private Volume blurVolume;
    [SerializeField] private float blurFadeSpeed = 5f;

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

    private int originalLayer;
    private int inspectingLayer;

    private void Start()
    {
        inspectingLayer = LayerMask.NameToLayer("Inspecting");
    }
    void Update()
    {
        if (currentItem == null || currentState == InspectionState.None) return;

        if (currentState == InspectionState.Inspecting)
        {
            HandleInspectingState();
        }
        else if (currentState == InspectionState.Returning)
        {
            HandleReturningState();
        }
    
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // limit distance
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

        currentItem = itemTransform;

        // save original position
        originalPos = currentItem.position;
        originalRot = currentItem.rotation;
        originalLayer = itemTransform.gameObject.layer; //original layer
        SetLayerRecursively(itemTransform.gameObject, inspectingLayer); //save layer for all children

        IsInspecting = true; //block other inputs
        currentState = InspectionState.Inspecting;

        FadeCrosshair(0f); //hide crosshair

        if (blurVolume != null) StartCoroutine(FadeVolume(1f));


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
        currentState = InspectionState.Returning;
        IsInspecting = false; // allow other inputs

        FadeCrosshair(1f); //show crosshair
        if (blurVolume != null) StartCoroutine(FadeVolume(0f));


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void FinishReturning()
    {
        //clean up physics and layers
        currentItem.position = originalPos;
        currentItem.rotation = originalRot;

        // reenable physics if needed
        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
            rb.isKinematic = false;

        // put it back on its original world layer
        SetLayerRecursively(currentItem.gameObject, originalLayer);

        currentItem = null;
        currentState = InspectionState.None;
    }

    private void FadeCrosshair(float targetAlpha)
    {
        // Stop the current fade if one is already running to avoid "jitter"
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(DoFade(targetAlpha));
    }

    private IEnumerator DoFade(float targetAlpha)
    {
        while (!Mathf.Approximately(crosshairGroup.alpha, targetAlpha))
        {
            crosshairGroup.alpha = Mathf.MoveTowards(crosshairGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private IEnumerator FadeVolume(float targetWeight)
    {
        while (!Mathf.Approximately(blurVolume.weight, targetWeight))
        {
            blurVolume.weight = Mathf.MoveTowards(blurVolume.weight, targetWeight, blurFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void HandleInspectingState()
    {
        HandleZoom();
        Vector3 targetPos = Camera.main.transform.position + (Camera.main.transform.forward * currentZoomDist);
        currentItem.position = Vector3.Lerp(currentItem.position, targetPos, Time.deltaTime * moveSpeed);

        HandleInput();

        if (Input.GetMouseButtonDown(1)) StopInspecting(); // right click to stop inspecting
    }

    private void HandleReturningState()
    {
        // smoothly move back to original spot
        currentItem.position = Vector3.Lerp(currentItem.position, originalPos, Time.deltaTime * moveSpeed);
        currentItem.rotation = Quaternion.Slerp(currentItem.rotation, originalRot, Time.deltaTime * moveSpeed);

        // check if the item is close enough to finish and snap back
        if (Vector3.Distance(currentItem.position, originalPos) < 0.01f &&
            Quaternion.Angle(currentItem.rotation, originalRot) < 1f)
        {
            FinishReturning();
        }
    }
}