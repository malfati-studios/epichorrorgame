﻿using System;
using UI;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Action<DoorEvent> doorListeners;

    [SerializeField] private Transform pivotPoint;
    [SerializeField] private string doorName;

    private FloatLerper lerper;
    private float lastLerpervalue;
    private bool playerNear;
    private BoxCollider _collider;
    private bool doorClosePlayed;
    private int openCount;
    private int closeCount;

    private enum DoorState
    {
        CLOSED,
        OPENING,
        OPEN,
        CLOSING
    }

    private DoorState currentState = DoorState.CLOSED;

    private void Update()
    {
        switch (currentState)
        {
            case DoorState.CLOSED:
                if (playerNear && Input.GetKeyDown(KeyCode.E)) Open();
                break;
            case DoorState.OPEN:
                if (playerNear && Input.GetKeyDown(KeyCode.E)) Close();
                break;
            case DoorState.OPENING:
                UpdateOpening();
                break;
            case DoorState.CLOSING:
                UpdateClosing();
                break;
        }
    }

    private void UpdateClosing()
    {
        lerper.Update();
        transform.RotateAround(pivotPoint.position, Vector3.up, lerper.CurrentValue - lastLerpervalue);
        lastLerpervalue = lerper.CurrentValue;

        if (!doorClosePlayed && lerper.CurrentValue < 10f)
        {
            AudioController.instance.PlayCloseDoorSound();
            doorClosePlayed = true;
        }

        if (lerper.Reached)
        {
            currentState = DoorState.CLOSED;
            lerper = null;
            _collider.enabled = true;
            doorClosePlayed = false;
        }
    }

    private void UpdateOpening()
    {
        lerper.Update();
        transform.RotateAround(pivotPoint.position, Vector3.up, lerper.CurrentValue - lastLerpervalue);
        lastLerpervalue = lerper.CurrentValue;
        if (lerper.Reached)
        {
            currentState = DoorState.OPEN;
            lerper = null;
            _collider.enabled = true;
        }
    }

    public void Close()
    {
        _collider.enabled = false;
        currentState = DoorState.CLOSING;
        lerper = new FloatLerper(2f, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        lerper.SetValues(90f, 0f, true);
        closeCount++;
        doorListeners.Invoke(new DoorEvent(doorName, DoorAction.CLOSE_DOOR, closeCount));
    }
    
    public void CloseFast()
    {
        _collider.enabled = false;
        currentState = DoorState.CLOSING;
        lerper = new FloatLerper(.5f, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        lerper.SetValues(90f, 0f, true);
        closeCount++;
        doorListeners.Invoke(new DoorEvent(doorName, DoorAction.CLOSE_DOOR, closeCount));
    }

    public void Open()
    {
        _collider.enabled = false;
        currentState = DoorState.OPENING;
        lerper = new FloatLerper(2f, AbstractLerper<float>.SMOOTH_TYPE.EASE_OUT);
        lerper.SetValues(0f, 90f, true);
        AudioController.instance.PlayOpenDoorSound();
        openCount++;
        doorListeners.Invoke(new DoorEvent(doorName, DoorAction.OPEN_DOOR, openCount));
    }

    private void OnTriggerEnter(Collider other)
    {
        playerNear = true;
        UIController.instance.ShowMessage("Press 'E' to use door");
    }

    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
        UIController.instance.HideMessage();
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
}

public struct DoorEvent
{
    public string doorName;
    public DoorAction action;
    public int eventCount;

    public DoorEvent(string doorName, DoorAction action, int eventCount)
    {
        this.doorName = doorName;
        this.action = action;
        this.eventCount = eventCount;
    }
}

public enum DoorAction
{
    OPEN_DOOR,
    CLOSE_DOOR
}