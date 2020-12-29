using Inventory;
using UI;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private string[] chestOpenedDialog;
    private bool playerNear;
    private bool opened;
    private bool looted;
    private PlayerInventory player;
    private Animation animation;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            player = other.GetComponent<PlayerInventory>();
            if (player.HasRustedKey())
            {
                UIController.instance.ShowMessage("Press 'Q' to open chest");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (opened) return;
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            player = null;
            UIController.instance.HideMessage();
        }
    }

    private void Update()
    {
        if (!playerNear) return;
        if (looted) return;

        if (opened && Input.GetKeyDown(KeyCode.Q))
        {
            looted = true;
            PlayerInventory.instance.PickUpLighter();
            UIController.instance.HideMessage();
            DialogManager.instance.ShowDialog(chestOpenedDialog);
            UIController.instance.ShowTorchesUI();
            transform.GetChild(2).gameObject.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q) && player.HasRustedKey())
        {
            UIController.instance.HideMessage();
            animation.Play();
            AudioController.instance.PLayOpenChestSound();
            Invoke("SetOpenedAfterAnimation", 2f);
        }
    }

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void SetOpenedAfterAnimation()
    {
        opened = true;
        UIController.instance.ShowMessage("Press 'Q' to pick up lighter");
    }
}