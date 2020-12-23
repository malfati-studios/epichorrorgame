using System;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public static DoorsController instance;
    public Action<DoorEvent> doorsEvents;

    [SerializeField] private Door keyRoomDoor;
    [SerializeField] private Door hallwayDoor;
    [SerializeField] private Door bedDoor;
    [SerializeField] private Door chestDoor;
    [SerializeField] private Door basementDoor;

    public void OpenDoor(string doorName)
    {
        FindDoorByName(doorName).Open();
    }

    public void CloseDoor(string doorName)
    {
        FindDoorByName(doorName).Close();
    }

    void Start()
    {
        SetUpDoors();
    }

    private void SetUpDoors()
    {
        keyRoomDoor.doorListeners += OnDoorEvent;
        hallwayDoor.doorListeners += OnDoorEvent;
        bedDoor.doorListeners += OnDoorEvent;
        chestDoor.doorListeners += OnDoorEvent;
        basementDoor.doorListeners += OnDoorEvent;
    }

    private void OnDoorEvent(DoorEvent doorEvent)
    {
        doorsEvents.Invoke(doorEvent);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Door FindDoorByName(string doorName)
    {
        if (keyRoomDoor.name.Equals(doorName))
        {
            return keyRoomDoor;
        }
        else if (hallwayDoor.name.Equals(doorName))
        {
            return hallwayDoor;
        }
        else if (bedDoor.name.Equals(doorName))
        {
            return bedDoor;
        }
        else if (chestDoor.name.Equals(doorName))
        {
            return chestDoor;
        }
        else if (basementDoor.name.Equals(doorName))
        {
            return basementDoor;
        }

        return null;
    }
}