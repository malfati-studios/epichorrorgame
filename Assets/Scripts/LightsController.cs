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

    public void TurnOffAllLights()
    {
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