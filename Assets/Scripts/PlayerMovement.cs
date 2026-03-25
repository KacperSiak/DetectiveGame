using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _moveDirtection;    
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (InspectSystem.IsInspecting) //check if player is inspecting an item
        {
            _moveDirtection = Vector3.zero;
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        _moveDirtection = new Vector3(x, 0f, z).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 globalMove = transform.TransformDirection(_moveDirtection);
        _rb.MovePosition(transform.position + globalMove * speed * Time.fixedDeltaTime);    
    }
}
