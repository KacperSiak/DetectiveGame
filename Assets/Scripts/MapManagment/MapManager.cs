using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public static Action<string> OnNewPlaceFind;

    [SerializeField] private GameObject map;
    [SerializeField] private Vector3 spawnForEleanor;
    [SerializeField] private Vector3 spawnForPharmacy;
    [SerializeField] private Vector3 spawnForWorkshop;
    [SerializeField] private Vector3 spawnForResidence;

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
        if (Input.GetKeyDown(KeyCode.M) && currentScene != "MainMenu")
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

    public void NewMapEntryFound(string foundScene)
    {
        OnNewPlaceFind?.Invoke(foundScene);
    }

    public void SetupCharakter(int ID)
    {
        switch(ID)
        {
            case 1:
                PlayerMovement.Instance.SetPosition(spawnForEleanor);
                break;
            case 2:
                PlayerMovement.Instance.SetPosition(spawnForPharmacy);
                break;
            case 3:
                PlayerMovement.Instance.SetPosition(spawnForWorkshop);
                break;
            case 4:
                PlayerMovement.Instance.SetPosition(spawnForResidence);
                break;

        }
    }

}
