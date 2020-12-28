using UnityEngine;

public class DialogEvent : MonoBehaviour, IEvent
{
    private bool canFire;
    private bool finished;
    [SerializeField] private string[] dialog;

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
        canFire = true;
    }

    public void DeactivateEvent()
    {
        finished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canFire && !finished && other.CompareTag("Player"))
        {
            DialogManager.instance.ShowDialog(dialog);
            finished = true;
        }
    }
}