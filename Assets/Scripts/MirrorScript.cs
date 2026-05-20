using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public Transform playerCamera;
    public Transform mirrorPlane;
    public Camera mirrorCamera;

    void LateUpdate()
    {
        Vector3 localPos = mirrorPlane.InverseTransformPoint(playerCamera.position);

        localPos.x *= -1;

        mirrorCamera.transform.position =
            mirrorPlane.TransformPoint(localPos);

        Vector3 localEuler = playerCamera.eulerAngles;
        mirrorCamera.transform.eulerAngles =
            new Vector3(localEuler.x, -localEuler.y, localEuler.z);
    }
}
