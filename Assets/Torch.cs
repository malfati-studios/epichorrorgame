using Inventory;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private GameObject light;
    private GameObject flames;

    void Start()
    {
        light = transform.GetChild(0).gameObject;
        light.SetActive(false);
        flames = transform.GetChild(1).gameObject;
        flames.SetActive(false);
    }

    public void TurnOn()
    {
        light.SetActive(true);
        flames.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerInventory>().StartPollingForTorch();
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerInventory>().StopPollingForTorch();
    }
}