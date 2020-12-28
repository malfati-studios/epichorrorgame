using System;
using UnityEngine;

public class TurnOffHallwayLightEvent : MonoBehaviour, IEvent
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject light;
    [SerializeField] private string[] playerReaction;
    private bool finished;

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
        Invoke("StartEvent", 3f);
    }

    public void DeactivateEvent()
    {
        finished = true;
    }

    private void StartEvent()
    {
        LightsController.instance.FlickerHallChandelier();
        AudioController.instance.PlayElectricitySound();
        AudioController.instance.PlayHighPitchedScarySound();
        CameraShake.ShakeCamera(.01f, 3.5f);
        Invoke("EndEvent", 3.5f);
    }

    private void EndEvent()
    {
        LightsController.instance.TurnOffHallChandelier();
        AudioController.instance.StopElectricitySound();
        finished = true;
        Invoke("OpenDoor", 2f);
    }

    private void OpenDoor()
    {
        door.Open();
        light.SetActive(true);
        Invoke("PlayerReaction", 2f);
    }

    private void PlayerReaction()
    {
        DialogManager.instance.ShowDialog(playerReaction);
    }

    public bool EventFinished()
    {
        return finished;
    }
}