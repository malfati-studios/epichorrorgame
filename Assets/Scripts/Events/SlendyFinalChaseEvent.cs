using Inventory;
using UnityEngine;

public class SlendyFinalChaseEvent : MonoBehaviour, IEvent
{
    private Slendy slendy;
    [SerializeField] private Transform slendySpawnPoint;

    private bool finished;
    private bool canFire;

    private void OnTriggerEnter(Collider other)
    {
        if (!finished && canFire)
        {
            PlayerInventory.instance.AllowThrowLighter();
            slendy.StartChase(slendySpawnPoint.position, PlayerMovement.instance.GetFeetPosition());
            PlayerLook.instance.ShakeCamera(.005f, 3f);
            LightsController.instance.FlickerAllLights();
            finished = true;
        }
    }

    public void SetUpEvent()
    {
        slendy = GameObject.FindGameObjectWithTag("Slendy").GetComponent<Slendy>();
        slendySpawnPoint = transform.GetChild(0);
    }

    public void FireEvent()
    {
        canFire = true;
    }

    public void DeactivateEvent()
    {
    }
}