using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float controlsSceneDuration;

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

    private void OnGameSceneLoad(string gameScene)
    {
        if (gameScene == SceneController.CONTROLS_SCENE)
        {
            Invoke("StartGame", controlsSceneDuration);
        }
        else if (gameScene == SceneController.GAME_SCENE)
        {
            EventManager.instance.GameStarted();
        }
    }

    private void StartGame()
    {
        AudioController.instance.FadeOutAmbience();
        SceneController.instance.LoadGameScene();
    }

    public void ShowControls()
    {
        SceneController.instance.LoadControlsScene();
    }
}