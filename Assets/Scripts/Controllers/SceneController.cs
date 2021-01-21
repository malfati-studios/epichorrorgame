using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region EXPOSED_FIELDS

    public static string GAME_SCENE = "GameScene";
    public static string CONTROLS_SCENE = "Controls";
    public static string MAIN_MENU = "MainMenu";
    public static string GAME_WON = "GameWon";
    public static string GAME_LOST = "GameLost";
    public static string CREDITS = "Credits";


    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private float transitionTime = 1.0f;

    public Action<string> levelLoaded;

    #endregion

    #region PRIVATE_FIELDS

    private FloatLerper alphaLerper = null;
    private string nextScene;

    #endregion

    #region ENUMS

    private enum STATE
    {
        IDLE,
        OPENING,
        CLOSING
    }

    private STATE state = STATE.IDLE;

    #endregion

    public static SceneController instance = null;

    #region UNITY_CALLS

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateTransition();
    }

    #endregion

    #region PUBLIC_METHODS

    public void LoadGameWonScene()
    {
        LoadSceneWithTransition(GAME_WON);
    }

    public void LoadGameLostScene()
    {
        LoadSceneWithTransition(GAME_LOST);
    }

    public void RestartGame()
    {
        Invoke("ResetGame", 3f);
    }

    private void ResetGame()
    {
        LoadSceneWithTransition(MAIN_MENU);
    }

    public void LoadGameScene()
    {
        LoadSceneWithTransition(GAME_SCENE);
    }

    public void LoadControlsScene()
    {
        LoadSceneWithTransition(CONTROLS_SCENE);
    }
    
    public void LoadMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        LoadSceneInstant(MAIN_MENU);
    }
    
    public void LoadCredits()
    {
        LoadSceneInstant(CREDITS);
    }

    #endregion

    #region PRIVATE_METHODS

    private void Initialize()
    {
        //si no hay ninguna instancia
        if (instance == null)
        {
            //YO soy la instancia
            instance = this;
            //no destruyo mi gameobject para que perdure entre escenas, y asi funcione la transicion visualmente
            DontDestroyOnLoad(gameObject);
            alphaLerper = new FloatLerper(transitionTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        }
        else
        {
            //sino me destruyo (porque otro es mi instance y no podemos tener 2)
            Destroy(gameObject);
        }
    }

    private void UpdateTransition()
    {
        if (!alphaLerper.On)
        {
            return;
        }

        alphaLerper.Update();
        canvasGroup.alpha = alphaLerper.CurrentValue;

        if (alphaLerper.Reached)
        {
            OnReached();
        }
    }

    private void OnReached()
    {
        switch (state)
        {
            case STATE.OPENING:
                OnTransitionEnd();
                break;
            case STATE.CLOSING:
                OnTransitionAtMid();
                break;
        }
    }

    private void OnTransitionStart()
    {
        //cuando arranca la transcion
        state = STATE.CLOSING;

        //tenemos que ir al alpha en 1, para poner todo negro
        alphaLerper.SetValues(canvasGroup.alpha, 1.0f, true);

        //bloqueamos la interaccion con la UI debajo de la UI de la transicion
        canvasGroup.blocksRaycasts = true;
    }

    private void OnTransitionAtMid()
    {
        //si el alpha llega a 1 estamos a la mitad
        state = STATE.OPENING;

        //aca hacemos el cambio real de la escena. Es un cambio sincronico, por lo que vamos a tener un mini cuelgue si la escena es pesada.
        SceneManager.LoadScene(nextScene);

        //ahora tenemos que ir devuelta a 0 alpha
        alphaLerper.SetValues(canvasGroup.alpha, 0.0f, true);
    }

    private void OnTransitionEnd()
    {
        //si el alpha esta en 0 termino la transicion, desbloqueamos la interaccion y ponemos el estado en idle para arrancar devuelta.
        canvasGroup.blocksRaycasts = false;
        state = STATE.IDLE;
        levelLoaded.Invoke(nextScene);
    }

    private void LoadSceneInstant(string sceneName)
    {
        //Guardamos la escena proxima y abrimos la transicion
        nextScene = sceneName;
        SceneManager.LoadScene(nextScene);
        levelLoaded.Invoke(nextScene);
    }

    private void LoadSceneWithTransition(string sceneName)
    {
        //Guardamos la escena proxima y abrimos la transicion
        nextScene = sceneName;
        OnTransitionStart();
    }

    #endregion
}