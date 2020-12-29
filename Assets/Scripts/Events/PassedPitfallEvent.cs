using UnityEngine;

namespace Events
{
    public class PassedPitfallEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private GameObject fakeDungeonWall;
        [SerializeField] private GameObject dungeonDoor;
        [SerializeField] private string[] playerReaction;

        private bool finished;

        public void SetUpEvent()
        {
        }

        public void FireEvent()
        {
        }

        public void DeactivateEvent()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!finished)
            {
                fakeDungeonWall.SetActive(false);
                dungeonDoor.SetActive(true);
                DialogManager.instance.ShowDialog(playerReaction);
                finished = true;
            }
        }
    }
}