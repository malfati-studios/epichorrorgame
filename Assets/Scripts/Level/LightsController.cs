using System;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public static LightsController instance;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    [SerializeField] private LevelLight enterLamp;
    [SerializeField] private LevelLight clockLamp;
    [SerializeField] private LevelLight cabinetLamp;
    [SerializeField] private LevelLight deskLamp;
    [SerializeField] private LevelLight livingChandelier;
    [SerializeField] private LevelLight hallChandelier;
    [SerializeField] private LevelLight roomLamp;
    [SerializeField] private GameObject openDoorSpotlight;


    private Material litMaterial;
    private Material unlitMaterial;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TurnOffAllLights();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TurnOnAllLights();
        }
    }

    public void TurnOffAllLights()
    {
        enterLamp.StartFlickering(minWaitTime, maxWaitTime);
        clockLamp.StartFlickering(minWaitTime, maxWaitTime);
        cabinetLamp.StartFlickering(minWaitTime, maxWaitTime);
        deskLamp.StartFlickering(minWaitTime, maxWaitTime);
        livingChandelier.StartFlickering(minWaitTime, maxWaitTime);
        hallChandelier.StartFlickering(minWaitTime, maxWaitTime);
        roomLamp.StartFlickering(minWaitTime, maxWaitTime);
        Invoke("InternalTurnOff", 2f);
    }

    private void InternalTurnOff()
    {
        openDoorSpotlight.SetActive(false);
        enterLamp.TurnOff();
        clockLamp.TurnOff();
        cabinetLamp.TurnOff();
        deskLamp.TurnOff();
        livingChandelier.TurnOff();
        hallChandelier.TurnOff();
        roomLamp.TurnOff();
    }


    public void TurnOnAllLights()
    {
        enterLamp.TurnOn();
        clockLamp.TurnOn();
        cabinetLamp.TurnOn();
        deskLamp.TurnOn();
        livingChandelier.TurnOn();
        hallChandelier.TurnOn();
        roomLamp.TurnOn();
    }


    public void FlickerHallChandelier()
    {
        hallChandelier.StartFlickering(minWaitTime, maxWaitTime);
    }

    public void TurnOnHallChandelier()
    {
        hallChandelier.TurnOn();
    }

    public void TurnOffHallChandelier()
    {
        hallChandelier.TurnOff();
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
}