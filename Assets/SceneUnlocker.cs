using UnityEngine;

public class SceneUnlocker : MonoBehaviour
{
    [SerializeField] private string sceneToUnlock;
    public void UnlockScene()
    {
        MapManager.Instance.NewMapEntryFound(sceneToUnlock);
    }
}
