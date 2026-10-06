using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public static Action<string> OnNewPlaceFind;

    [SerializeField] private GameObject map;

    public string currentScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    private void Start()
    {
        map.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.gameObject.SetActive(!map.activeSelf);
           if (map.activeSelf)
           {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
           }
           else
           {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }

        }

    }

    public bool MoveValidator(string scene)
    {
        if (currentScene == scene) return false;
        else return true;
    }

    public void NewMApEntryFound(string foundScene)
    {
        OnNewPlaceFind?.Invoke(foundScene);
    }

}
