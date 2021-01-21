using Inventory;
using UnityEngine;

public abstract class PickupableObject : MonoBehaviour
{
    private bool playerNear;
    private PlayerInventory player;

    public abstract void PickUpCallback(PlayerInventory player);
    public abstract void PlayerNearCallback(PlayerInventory player);
    public abstract void PlayerLeftCallback(PlayerInventory player);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            player = other.GetComponent<PlayerInventory>();
            PlayerNearCallback(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            PlayerLeftCallback(player);
        }
    }

    private void Update()
    {
        if (!playerNear) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PickUpCallback(player);
            Destroy(gameObject);
        }
    }
}