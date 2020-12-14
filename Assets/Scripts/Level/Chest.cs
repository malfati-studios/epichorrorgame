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
                UIController.instance.ShowOpenChestText();
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
            UIController.instance.HideOpenChestText();
        }
    }

    private void Update()
    {
        if (!playerNear) return;
        if (opened) return;
        if (Input.GetKeyDown(KeyCode.Q) && player.HasRustedKey())
        {
            UIController.instance.HideOpenChestText();
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