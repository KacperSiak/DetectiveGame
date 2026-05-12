using UnityEngine;
using UnityEngine.UI;

public class InspectRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image crosshair;
    [SerializeField] private InspectSystem inspectSystem;

    private ObjectController raycastedObj;
    private OutlineForClues currentOutline;
    private bool isHovering;

    void Update()
    {
        // if inspecting, just stop this code
        if (InspectSystem.IsInspecting) return;

        HandleRaycast();
    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractObject"))
            {
                // optimization - only do this if we aren't already hovering over this specific object
                if (!isHovering)
                {
                    ObjectFound(hit.collider.gameObject);
                }

                // handle the click
                if (Input.GetMouseButtonDown(0))
                {
                    // clean up the hover state
                    ClearSelection();
                    inspectSystem.StartInspecting(hit.collider.transform);
                }
            }
            else if (isHovering)
            {
                ClearSelection();
            }
        }
        else if (isHovering)
        {
            ClearSelection();
        }
    }

    private void ObjectFound(GameObject obj)
    {
        isHovering = true;
        raycastedObj = obj.GetComponent<ObjectController>();

        if (raycastedObj != null)
        {
            raycastedObj.ShowObjectName();
        }

        if (obj.TryGetComponent<OutlineForClues>(out OutlineForClues outline))
        {
            currentOutline = outline;
            currentOutline.enabled = true;
        }

        CrosshairChange(true);
    }

    public void ClearSelection()
    {
        if (!isHovering) return; // don't clear if nothing is selected

        if (raycastedObj != null)
        {
            raycastedObj.HideObjectName();
            raycastedObj = null;
        }

        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }

        CrosshairChange(false);
        isHovering = false;
    }

    private void CrosshairChange(bool on)
    {
        crosshair.color = on ? Color.red : Color.white;
    }
}