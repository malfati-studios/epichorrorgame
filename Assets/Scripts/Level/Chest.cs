using Inventory;
using UI;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerNear;
    private bool opened;
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
        if (opened) return;
        if (Input.GetKeyDown(KeyCode.Q) && player.HasRustedKey())
        {
            UIController.instance.HideMessage();
            animation.Play();
            AudioController.instance.PLayOpenChestSound();
            player.OpenedChest();
            opened = true;    
        }
    }

    private void Start()
    {
        animation = GetComponent<Animation>();
    }
}