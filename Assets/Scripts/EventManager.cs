using Events;
using Inventory;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private IScaryEvent hallwayLightEvent;
    private IScaryEvent allLightsOffEvent;
    private IScaryEvent slendyScream;
    private IScaryEvent dungeonDoorAppearEvent;


    public void GameStarted()
    {
        DialogManager.instance.StartingDialog();
    }

    private void OnDoorEvent(DoorEvent doorEvent)
    {
        if (doorEvent.doorName.Equals("hallwayDoor") && doorEvent.action.Equals(DoorAction.OPEN_DOOR) &&
            doorEvent.eventCount == 1)
        {
            hallwayLightEvent.FireEvent();
        }
    }

    private void Start()
    {
        DoorsController.instance.doorsEvents += OnDoorEvent;
        GameObject.Find("KeyRoomNote").GetComponent<Note>().noteRead += OnKeyRoomNoteRead;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventoryEvents += OnInventoryEvent;
        SetUpEvents();
    }

    private void OnKeyRoomNoteRead(Note.NoteName obj)
    {
        dungeonDoorAppearEvent.FireEvent();
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
                allLightsOffEvent.FireEvent();
                break;
        }
    }

    private void SetUpEvents()
    {
        hallwayLightEvent = transform.GetChild(0).GetChild(0).GetComponent<IScaryEvent>();
        hallwayLightEvent.SetUpEvent();
        allLightsOffEvent = transform.GetChild(0).GetChild(1).GetComponent<IScaryEvent>();
        allLightsOffEvent.SetUpEvent();
        dungeonDoorAppearEvent =  transform.GetChild(0).GetChild(2).GetComponent<IScaryEvent>();
        dungeonDoorAppearEvent.SetUpEvent();
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