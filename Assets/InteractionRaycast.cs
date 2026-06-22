using UnityEngine;
using DG.Tweening;

public class InteractionRaycast : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask padLockLayer;
    [SerializeField] private LayerMask moveableOjectLayer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 cameraPlace;

    private PlayerMovement _pm;
    private CameraLook _cl;
    

    private void Awake()
    {
        _pm = GetComponentInParent<PlayerMovement>();
        _cl = GetComponent<CameraLook>();
    }
    void Update()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, padLockLayer))
            {
                PadLockCameraFocus padlock = hit.collider.GetComponent<PadLockCameraFocus>();

                if(padlock != null)
                {
                    padlock.EnterFocusMode(gameObject, this);
                    _cl.enabled = false;
                    _pm.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            
            if (Physics.Raycast(ray, out hit, interactionDistance, moveableOjectLayer))
            {
                MoveableObject objectToMove = hit.collider.GetComponent<MoveableObject>();
                
                if(objectToMove != null)
                {
                    objectToMove.MoveMe();
                }
            }
        }
    }

    public void GoBack(GameObject camera)
    {
        _cl.enabled = true;
        _pm.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camera.transform.SetParent(player.transform, false);
        camera.transform.localPosition = cameraPlace;
    }
}

