using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private float rayDistance = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            hit.collider.TryGetComponent<DoorController>(out var door);
            door.InteractWithDoor();
        }
    }

}
