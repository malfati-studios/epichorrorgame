using UnityEngine;

//This class gonna be ugly
public class EventManager : MonoBehaviour
{
    private IScaryEvent slendyInHallway;

    private void Start()
    {
        DoorsController.instance.doorsEvents += OnDoorEvent;
        SetUpEvents();
    }

    private void SetUpEvents()
    {
        slendyInHallway = transform.GetChild(0).GetChild(0).GetComponent<IScaryEvent>();
        slendyInHallway.SetUpEvent();
    }

    private void OnDoorEvent(DoorEvent doorEvent)
    {
        if (doorEvent.doorName.Equals("hallwayDoor") && doorEvent.action.Equals(DoorAction.OPEN_DOOR) &&
            doorEvent.eventCount == 1)
        {
            slendyInHallway.FireEvent();
        }
    }
}