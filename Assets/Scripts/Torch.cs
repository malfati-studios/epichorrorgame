using Inventory;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private GameObject light;
    private GameObject flames;
    private bool lit;
    private PlayerInventory player;

    void Start()
    {
        light = transform.GetChild(0).gameObject;
        light.SetActive(false);
        flames = transform.GetChild(1).gameObject;
        flames.SetActive(false);
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
        player.StopPollingForTorch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lit) return;
        player = other.GetComponent<PlayerInventory>();
        other.GetComponent<PlayerInventory>().StartPollingForTorch();
    }

    private void OnTriggerExit(Collider other)
    {
        if (lit) return;
        other.GetComponent<PlayerInventory>().StopPollingForTorch();
    }
}