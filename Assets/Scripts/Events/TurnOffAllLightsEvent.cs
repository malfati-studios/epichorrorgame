using UnityEngine;

public class TurnOffAllLightsEvent : MonoBehaviour, IScaryEvent
{
    private bool canFire;
    private bool finished;
    [SerializeField] private GameObject bedRoomGrunge;
    [SerializeField] private GameObject mainDoor;
    [SerializeField] private GameObject fakeWall;
    [SerializeField] private GameObject crickets;
    [SerializeField] private GameObject lightBlock1;
    [SerializeField] private GameObject lightBlock2;


    public void SetUpEvent()
    {
        fakeWall.SetActive(false);
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
            mainDoor.SetActive(false);
            fakeWall.SetActive(true);
            crickets.SetActive(false);
            lightBlock1.SetActive(true);
            lightBlock2.SetActive(true);
            finished = true;
        }
    }
}