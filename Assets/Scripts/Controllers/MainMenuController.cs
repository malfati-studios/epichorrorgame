using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private LevelLight mainMenuLight;

    private void Start()
    {
        mainMenuLight.StartFlickering(0.1f, 0.6f);
        AudioController.instance.PlayAmbience();
    }

    public void OnPlayButtonStarted()
    {
        GameManager.instance.ShowControls();
    }
    
    public void OnCreditsButtonPreseds()
    {
        GameManager.instance.ShowCredits();
    }
    
    public void OnQuitButtonPreseds()
    {
        Application.Quit();
    }
}