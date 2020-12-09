using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject openDoorText;
    [SerializeField] private GameObject pickupFlashlightText;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject toggleFlashlightText;



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

    public void ShowOpenDoorText()
    {
        openDoorText.SetActive(true);
    }

    public void ShowFlashlightTutorialText()
    {
        toggleFlashlightText.SetActive(true);
        Invoke("HideFlashlightTutorialText", 3f);
    }
    
    public void HideFlashlightTutorialText()
    {
        toggleFlashlightText.SetActive(false);
    }

    public void HideOpenDoorText()
    {
        openDoorText.SetActive(false);
    }

    public void ShowPickUpObjectText(string objectName)
    {
       pickupFlashlightText.SetActive(true);
       pickupFlashlightText.GetComponent<TextMeshProUGUI>().text = "Press 'Q' to pick up " + objectName;
    }

    public void HidePickUpObjectText()
    {
        pickupFlashlightText.SetActive(false);
    }

    public void ActivateFlashlightInventory()
    {
        inventory.SetActive(true);
        inventory.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ActivateRustedKeyInventory()
    {
        inventory.SetActive(true);
        inventory.transform.GetChild(1).gameObject.SetActive(true);
    }
}