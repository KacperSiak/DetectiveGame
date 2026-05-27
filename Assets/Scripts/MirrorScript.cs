using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform playerCamera; 
    [SerializeField] private Transform mirrorCamera; 

    void LateUpdate()
    {
        if (playerCamera == null || mirrorCamera == null) return;

        Vector3 localPlayerPos = transform.InverseTransformPoint(playerCamera.position);
        localPlayerPos.z = -localPlayerPos.z;
        mirrorCamera.position = transform.TransformPoint(localPlayerPos);

        Vector3 localPlayerForward = transform.InverseTransformDirection(playerCamera.forward);
        Vector3 localPlayerUp = transform.InverseTransformDirection(playerCamera.up);

        localPlayerForward.z = -localPlayerForward.z;
        localPlayerUp.z = -localPlayerUp.z;

        Vector3 worldLookDir = transform.TransformDirection(localPlayerForward);
        Vector3 worldUpDir = transform.TransformDirection(localPlayerUp);

        mirrorCamera.rotation = Quaternion.LookRotation(worldLookDir, worldUpDir);
    }
}