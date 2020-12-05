using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform pivotPoint;

    private FloatLerper lerper;
    private float lastLerpervalue;
    private bool playerNear;
    private BoxCollider _collider;
    private bool doorClosePlayed;

    private enum DoorState
    {
        CLOSED,
        OPENING,
        OPEN,
        CLOSING
    }

    private DoorState currentState = DoorState.CLOSED;

    private void Update()
    {
        switch (currentState)
        {
            case DoorState.CLOSED:
                if (playerNear && Input.GetKeyDown(KeyCode.E)) Open();
                break;
            case DoorState.OPEN:
                if (playerNear && Input.GetKeyDown(KeyCode.E)) Close();
                break;
            case DoorState.OPENING:
                UpdateOpening();
                break;
            case DoorState.CLOSING:
                UpdateClosing();
                break;
        }
    }

    private void UpdateClosing()
    {
        lerper.Update();
        transform.RotateAround(pivotPoint.position, Vector3.up, lerper.CurrentValue - lastLerpervalue);
        lastLerpervalue = lerper.CurrentValue;

        if (!doorClosePlayed && lerper.CurrentValue < 10f)
        {
            AudioController.instance.PlayCloseDoorSound();
            doorClosePlayed = true;
        }

        if (lerper.Reached)
        {
            currentState = DoorState.CLOSED;
            lerper = null;
            _collider.enabled = true;
            doorClosePlayed = false;
        }
    }

    private void UpdateOpening()
    {
        lerper.Update();
        transform.RotateAround(pivotPoint.position, Vector3.up, lerper.CurrentValue - lastLerpervalue);
        lastLerpervalue = lerper.CurrentValue;
        if (lerper.Reached)
        {
            currentState = DoorState.OPEN;
            lerper = null;
            _collider.enabled = true;
        }
    }

    private void Close()
    {
        _collider.enabled = false;
        currentState = DoorState.CLOSING;
        lerper = new FloatLerper(2f, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        lerper.SetValues(90f, 0f, true);
    }

    private void Open()
    {
        _collider.enabled = false;
        currentState = DoorState.OPENING;
        lerper = new FloatLerper(2f, AbstractLerper<float>.SMOOTH_TYPE.EASE_OUT);
        lerper.SetValues(0f, 90f, true);
        AudioController.instance.PlayOpenDoorSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerNear = true;
        UIController.instance.ShowOpenDoorText();
    }

    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
        UIController.instance.HideOpenDoorText();
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
}