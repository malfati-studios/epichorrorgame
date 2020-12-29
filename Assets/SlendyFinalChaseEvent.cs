using UnityEngine;

public class SlendyFinalChaseEvent : MonoBehaviour, IEvent
{
    private Slendy slendy;
    [SerializeField] private Transform slendySpawnPoint;

    private bool finished;
    // Start is called before the first frame update

    public void Start()
    {
        slendy = GameObject.FindGameObjectWithTag("Slendy").GetComponent<Slendy>();
        slendySpawnPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!finished)
        {
            FireEvent();
        }
    }

    public void SetUpEvent()
    {
    }

    public void FireEvent()
    {
        slendy.StartWalking(slendySpawnPoint.position, PlayerMovement.instance.GetFeetPosition());
        CameraShake.ShakeCamera(.1f, 3f);
        LightsController.instance.FlickerAllLights();
        finished = true;
    }

    public void DeactivateEvent()
    {
    }
}