using UnityEngine;
using UnityEngine.UI;

public enum EvidenceState
{
    Normal,
    Excluded,
    Highligted
}

public class CluesStateChecker : MonoBehaviour
{
    public EvidenceState State;

    [SerializeField] private Image line;
    [SerializeField] private Image circle;

    private void Start()
    {
        State = EvidenceState.Normal;
        line.gameObject.SetActive(false);
        circle.gameObject.SetActive(false);
    }

    public void ChangeState()
    {
        State = (EvidenceState)(((int)State + 1) % 3);

        switch(State)
        {
            case EvidenceState.Normal:
                line.gameObject.SetActive(false);
                circle.gameObject.SetActive(false);
                break;
            
            case EvidenceState.Excluded:
                line.gameObject.SetActive(true);
                circle.gameObject.SetActive(false);
                break;

            case EvidenceState.Highligted:
                line.gameObject.SetActive(false);
                circle.gameObject.SetActive(true);
                break;
        }
    }
}
