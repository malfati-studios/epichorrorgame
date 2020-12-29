using UnityEngine;

public class FallKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LostTheGame();
        }
    }
}
