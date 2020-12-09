public class RustedKey : PickupableObject
{
    private static string RUSTED_KEY = "rusted key";
    // Update is called once per frame
    public override void PickUpCallback(PlayerInventory player)
    {
        UIController.instance.HidePickUpObjectText();
        player.PickUpRustedKey();
        AudioController.instance.PlayGrabKeySound();
    }

    public override void PlayerNearCallback()
    {
        UIController.instance.ShowPickUpObjectText(RUSTED_KEY);
    }

    public override void PlayerLeftCallback()
    {
        UIController.instance.HidePickUpObjectText();
    }

}
