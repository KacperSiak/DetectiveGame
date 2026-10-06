using UnityEngine;

public class DragLockDial : MonoBehaviour
{
    [Header("Ustawienia Obrotu")]
    [SerializeField] private float sensitivity = 0.4f; 
    [SerializeField] private float snapSpeed = 12f;

    [SerializeField] private int myPlace;

    private PadLockController _plc;
    private float currentAngle = 0f;
    private float targetAngle = 0f;
    private bool isDragging = false;
    private int currentValue = 0;

    private void Awake()
    {
        _plc = GetComponentInParent<PadLockController>();
    }

    void Update()
    {
        if (!isDragging)
        {
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * snapSpeed);
            ApplyRotation(currentAngle);
        }
    }


    void OnMouseDrag()
    {
        isDragging = true;

        float mouseInput = Input.GetAxis("Mouse X") * sensitivity * 100f;

        currentAngle += mouseInput;
        ApplyRotation(currentAngle);
    }


    void OnMouseUp()
    {
        isDragging = false;

        float normalizedAngle = currentAngle % 360f;
        if (normalizedAngle < 0) normalizedAngle += 360f;


        int nearestIndex = Mathf.RoundToInt(normalizedAngle / 36f);
        int placeValue = nearestIndex % 10;

        if (placeValue == 0) currentValue = 0;
        else currentValue = 10 - placeValue;

        _plc.GetNewNumber(myPlace, currentValue);

        targetAngle = nearestIndex * 36f;
    }

    private void ApplyRotation(float angle)
    {
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }
}
