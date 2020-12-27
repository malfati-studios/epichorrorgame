using UnityEngine;

public class TurnOffAllLightsEvent : MonoBehaviour, IScaryEvent
{
    private bool canFire;
    private bool finished;
    [SerializeField] private GameObject bedRoomGrunge;

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
        canFire = true;
    }

    public bool EventFinished()
    {
        return finished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canFire && other.CompareTag("Player") && !finished)
        {
            LightsController.instance.TurnOffAllLights();
            AudioController.instance.PlayHorrorAmbience();
            bedRoomGrunge.SetActive(true);
            finished = true;
        }
    }
}