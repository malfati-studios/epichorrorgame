using UnityEngine;

namespace Events
{
    public class EnterDungeonEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private GameObject trippyWall;
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
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!finished && other.CompareTag("Player"))
            {
                trippyWall.SetActive(true);
                DialogManager.instance.ShowDialog(playerReaction);
                EventManager.instance.NotifyEnteredDungeon();
                finished = true;
            }
        }
    }
}