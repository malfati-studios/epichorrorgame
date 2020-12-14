using System;
using Events;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Action<EventName> inventoryEvents;
    
    [SerializeField] private GameObject mainCamera;

    private bool hasFlashlight;
    private bool hasRustedKey;
    private bool activeFlashLight;

    public void PickUpFlashlight()
    {
        UIController.instance.ActivateFlashlightInventory();
        UIController.instance.ShowFlashlightTutorialText();
        mainCamera.transform.GetChild(0).gameObject.SetActive(true);
        hasFlashlight = true;
        inventoryEvents.Invoke(EventName.PICK_UP_FLASHLIGHT);
    }

    public bool HasFlashlight()
    {
        return hasFlashlight;
    }

    public bool HasRustedKey()
    {
        return hasRustedKey;
    }

    public void PickUpRustedKey()
    {
        UIController.instance.ActivateRustedKeyInventory();
        hasRustedKey = true;
        inventoryEvents.Invoke(EventName.PICK_UP_RUSTED_KEY);
    }

    public void UseRustedKey()
    {
        UIController.instance.DeactivateRustedKeyInventory();
        hasRustedKey = false;
    }

    private void Update()
    {
        if (hasFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                activeFlashLight = !activeFlashLight;
                mainCamera.transform.GetChild(0).GetChild(0).gameObject.SetActive(activeFlashLight);
                AudioController.instance.PlayFlashlightSound();
            }
        }
    }

    public void OpenedChest()
    {
    }
}