using UnityEngine;

public class FallKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneController.instance.LoadMainMenu();
        }
    }
}
