using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneController.instance.levelLoaded += OnGameSceneLoad;
    }

    private void OnGameSceneLoad(string obj)
    {
        DialogManager.instance.StartingDialog();
    }

    public void StartGame()
    {
        SceneController.instance.LoadGameScene();
    }
}