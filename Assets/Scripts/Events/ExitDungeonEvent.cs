using UI;
using UnityEngine;

namespace Events
{
    public class ExitDungeonEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private GameObject slendySpawnPoint;
        [SerializeField] private GameObject dungeonBlockWall;
        [SerializeField] private Door door;
        [SerializeField] private LevelLight houseLight;
        [SerializeField] private GameObject hallWayFurniture;
        [SerializeField] private GameObject hallwayGrunge;
        
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
                hallwayGrunge.SetActive(true);
                hallWayFurniture.SetActive(false);
                slendy.Scream(slendySpawnPoint.transform.position);
                finished = true;
                Invoke("CloseDoor", 1f);
            }
        }

        private void CloseDoor()
        {
            door.CloseFast();
            Invoke("TeachToKillSlenderman", 6f);
        }

        private void TeachToKillSlenderman()
        {
            UIController.instance.ShowTimedMessage("Press right mouse click to throw the lighter to Slenderman!", 5f);
        }
    }
}