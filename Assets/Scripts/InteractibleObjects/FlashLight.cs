﻿using Inventory;
using UI;

public class FlashLight : PickupableObject
{
    private static string FLASHLIGHT = "Flashlight";
    
    public override void PickUpCallback(PlayerInventory player)
    {
        UIController.instance.HideMessage();
        player.PickUpFlashlight();
    }

    public override void PlayerNearCallback(PlayerInventory player)
    {
        UIController.instance.ShowPickUpObjectText(FLASHLIGHT);
    }

    public override void PlayerLeftCallback(PlayerInventory player)
    {
        UIController.instance.HideMessage();
    }
}