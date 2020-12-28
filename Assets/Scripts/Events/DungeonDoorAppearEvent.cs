using UnityEngine;

public class DungeonDoorAppearEvent : MonoBehaviour, IEvent
{
    [SerializeField] private GameObject dungeonDoor1;
    [SerializeField] private GameObject dungeonDoor2;
    [SerializeField] private GameObject fakeWall;
    private bool finished;

    public void SetUpEvent()
    {
        fakeWall.SetActive(true);
        dungeonDoor1.SetActive(false);
        dungeonDoor2.SetActive(false);
    }

    public void FireEvent()
    {
        if (!finished)
        {
            dungeonDoor1.SetActive(true);
            dungeonDoor2.SetActive(true);
            fakeWall.SetActive(false);
            finished = true;
        }
    }

    public void DeactivateEvent()
    {
        finished = true;
    }
}