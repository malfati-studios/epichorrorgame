using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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