using UnityEngine;

namespace Events
{
    public class ExitDungeonEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private GameObject slendySpawnPoint;
        [SerializeField] private GameObject dungeonBlockWall;
        [SerializeField] private Door door;

        private Slendy slendy;
        private bool canFire;
        private bool finished;

        public void SetUpEvent()
        {
            slendy = GameObject.FindGameObjectWithTag("Slendy").GetComponent<Slendy>();
        }

        public void FireEvent()
        {
            canFire = true;
            dungeonBlockWall.SetActive(false);
        }

        public void DeactivateEvent()
        {
            finished = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!finished && canFire)
            {
                slendy.Scream(slendySpawnPoint.transform.position);
                finished = true;
                Invoke("CloseDoor", 1f);
            }
        }

        private void CloseDoor()
        {
            door.CloseFast();
        }
    }
}