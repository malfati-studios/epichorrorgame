using System;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField] private GameObject enterLamp;
    [SerializeField] private GameObject clockLamp;
    [SerializeField] private GameObject cabinetLamp;
    [SerializeField] private GameObject deskLamp;
    [SerializeField] private GameObject livingChandelier;
    [SerializeField] private GameObject hallChandelier;
    [SerializeField] private GameObject roomLamp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnOffAllLights();
        }
    }

    public void TurnOffAllLights()
    {
        TurnOffLight(enterLamp);
        TurnOffLight(clockLamp);
        TurnOffLight(cabinetLamp);
        TurnOffLight(deskLamp);
        TurnOffLight(livingChandelier);
        TurnOffLight(hallChandelier);
        TurnOffLight(roomLamp);
    }

    public void TurnOnLight(GameObject light)
    {
        light.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void TurnOffLight(GameObject light)
    {
        light.transform.GetChild(0).gameObject.SetActive(false);
    }
}