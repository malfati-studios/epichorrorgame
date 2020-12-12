using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private LevelLight mainMenuLight;
    [SerializeField] private AudioSource ambience;

    private void Start()
    {
        mainMenuLight.StartFlickering(0.1f, 0.6f);
        ambience.Play();
    }

    public void OnPlayButtonStarted()
    {
        GameManager.instance.StartGame();
        StartCoroutine(FadeAudioSource.StartFade(ambience,1f, 0));
    }
    
    
}