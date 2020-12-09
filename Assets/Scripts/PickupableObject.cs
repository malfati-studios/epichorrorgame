using UnityEngine;

public abstract class PickupableObject : MonoBehaviour
{
    private bool playerNear;
    private PlayerInventory player;

    public abstract void PickUpCallback(PlayerInventory player);
    public abstract void PlayerNearCallback();
    public abstract void PlayerLeftCallback();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            player = other.GetComponent<PlayerInventory>();
            PlayerNearCallback();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            player = null;
            PlayerLeftCallback();
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