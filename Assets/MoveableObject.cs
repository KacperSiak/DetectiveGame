using UnityEngine;
using DG.Tweening;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private Vector3 placeToMove;
    [SerializeField] private float moveSpeed;

    public void MoveMe()
    {
        transform.DOMove(placeToMove, moveSpeed);
    }
}
