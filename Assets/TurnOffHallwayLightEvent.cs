using System;
using UnityEngine;

public class TurnOffHallwayLightEvent : MonoBehaviour, IScaryEvent
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject light;
    private bool finished;

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
        Invoke("StartEvent", 3f);
    }

    private void StartEvent()
    {
        LightsController.instance.FlickerHallChandelier();
        AudioController.instance.PlayFirstJumpscareSound();
        Invoke("EndEvent", 3.5f);
    }

    private void EndEvent()
    {
        LightsController.instance.TurnOffHallChandelier();
        finished = true;
        Invoke("OpenDoor", 2f);
    }

    private void OpenDoor()
    {
        door.Open();
        light.SetActive(true);
    }

    public bool EventFinished()
    {
        return finished;
    }
}