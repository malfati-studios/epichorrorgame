using Events;
using UnityEngine;

//This class gonna be ugly
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private IScaryEvent slendyInHallway;

    public void GameStarted()
    {
        DialogManager.instance.StartingDialog();
    }

    private void OnDoorEvent(DoorEvent doorEvent)
    {
        if (doorEvent.doorName.Equals("hallwayDoor") && doorEvent.action.Equals(DoorAction.OPEN_DOOR) &&
            doorEvent.eventCount == 1)
        {
            slendyInHallway.FireEvent();
        }
    }


    // Startups
    private void Start()
    {
        DoorsController.instance.doorsEvents += OnDoorEvent;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventoryEvents += OnInventoryEvent;
        SetUpEvents();
    }

    private void OnInventoryEvent(EventName eventName)
    {
        switch (eventName)
        {
            case EventName.PICK_UP_RUSTED_KEY:
                DialogManager.instance.GetRustedKeyDialog();
                break;
            case EventName.PICK_UP_FLASHLIGHT:
                DialogManager.instance.GetFlashLightDialog();
                break;
        }
    }

    private void SetUpEvents()
    {
        slendyInHallway = transform.GetChild(0).GetChild(0).GetComponent<IScaryEvent>();
        slendyInHallway.SetUpEvent();
    }

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
}