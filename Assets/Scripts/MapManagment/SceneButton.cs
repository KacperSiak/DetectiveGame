using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private GameObject blocker;

    private void Awake()
    {
        MapManager.OnNewPlaceFind += CheckBlocker;
    }

    public void MoveToScene()
    {
        if (sceneName != null) 
        { 
            if(MapManager.Instance.MoveValidator(sceneName))
            {
                SceneManager.LoadScene(sceneName);
                MapManager.Instance.currentScene = sceneName;
            }
        }
        else Debug.LogError("There is no Scene name");
    }

    private void CheckBlocker(string foundSceneName)
    {
        if(foundSceneName == sceneName) blocker.SetActive(false);
    }

    private void OnDestroy()
    {
        MapManager.OnNewPlaceFind -= CheckBlocker;
    }
}
