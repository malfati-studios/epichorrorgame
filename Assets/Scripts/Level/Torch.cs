using Inventory;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private GameObject light;
    private GameObject flames;
    private GameObject sound;
    private bool lit;

    void Start()
    {
        light = transform.GetChild(0).gameObject;
        light.SetActive(false);
        flames = transform.GetChild(1).gameObject;
        flames.SetActive(false);
        sound = transform.GetChild(2).gameObject;
    }

    public bool IsLit()
    {
        return lit;
    }

    public void TurnOn()
    {
        light.SetActive(true);
        flames.SetActive(true);
        TorchesController.instance.NotifyTorchLit();
        lit = true;
        PlayerLook.instance.StopPollingForTorch();
        sound.SetActive(true);
        sound.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lit) return;
        PlayerLook.instance.StartPollingForTorch();
    }

    private void OnTriggerExit(Collider other)
    {
        if (lit) return;
        PlayerLook.instance.StopPollingForTorch();
    }
}