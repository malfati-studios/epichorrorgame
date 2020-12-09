public class FlashLight : PickupableObject
{
    private static string FLASHLIGHT = "Flashlight";
    
    public override void PickUpCallback(PlayerInventory player)
    {
        UIController.instance.HidePickUpObjectText();
        player.PickUpFlashlight();
    }

    public override void PlayerNearCallback()
    {
        UIController.instance.ShowPickUpObjectText(FLASHLIGHT);
    }

    public override void PlayerLeftCallback()
    {
        UIController.instance.HidePickUpObjectText();
    }
}