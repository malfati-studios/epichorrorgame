using UnityEngine;

public class PlayAgainButton : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneController.instance.LoadMainMenu();
    }
}
