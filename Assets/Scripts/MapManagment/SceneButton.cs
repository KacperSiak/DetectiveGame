using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName;

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
}
