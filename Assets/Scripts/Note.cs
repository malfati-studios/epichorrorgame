using Inventory;
using UI;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum NoteName
    {
        LIVINGROOMNOTE,
        KEYROOMNOTE
    }

    [SerializeField] private NoteName noteName;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerInventory>().StartPollingForPLayerLookNote();
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerInventory>().StopPollingForPLayerLookNote();
    }

    public void ShowNote()
    {
        switch (noteName)
        {
            case NoteName.LIVINGROOMNOTE:
                UIController.instance.ShowLivingRoomNote();
                return;
            case NoteName.KEYROOMNOTE:
                UIController.instance.ShowKeyRoomNote();
                return;
        }
    }
}