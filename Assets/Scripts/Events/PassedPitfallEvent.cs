using UnityEngine;

namespace Events
{
    public class PassedPitfallEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private GameObject fakeDungeonWall;
        [SerializeField] private GameObject dungeonDoor;
        [SerializeField] private string[] playerReaction;
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
            fakeDungeonWall.SetActive(false);
            dungeonDoor.SetActive(true);
            DialogManager.instance.ShowDialog(playerReaction);
        }
    }
}
