using UnityEngine;

public class EnterDungeonEvent : MonoBehaviour, IScaryEvent
{
    [SerializeField] private GameObject trippyWall;

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trippyWall.SetActive(true);
        }
    }
}