using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void MoveToScene()
    {
        if(sceneName != null) SceneManager.LoadScene(sceneName);
    }
}
