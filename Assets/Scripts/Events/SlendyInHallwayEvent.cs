using System;
using UnityEngine;

namespace Events
{
    public class SlendyInHallwayEvent : MonoBehaviour, IScaryEvent
    {
        private Slendy slendy;
        private Transform slendySpawnPoint;
        private Transform slendyEndPoint;
        [SerializeField] private Door door;
        [SerializeField] private GameObject light;


        private float eventDelay = 2f;
        private DateTime eventStartedTime;
        private bool finished;

        public void SetUpEvent()
        {
            slendy = GameObject.FindGameObjectWithTag("Slendy").GetComponent<Slendy>();
            slendySpawnPoint = transform.GetChild(0);
            slendyEndPoint = transform.GetChild(1);
        }

        public void FireEvent()
        {
            Invoke("StartEvent", eventDelay);
        }

        public bool EventFinished()
        {
            return finished;
        }

        private void StartEvent()
        {
            LightsController.instance.FlickerHallChandelier();
            AudioController.instance.PlayFirstJumpscareSound();
            slendy.StartWalking(slendySpawnPoint.position, slendyEndPoint.position);
            Invoke("EndEvent", 3f);
        }

        private void EndEvent()
        {
            LightsController.instance.TurnOffHallChandelier();
            door.Open();
            light.SetActive(true);
            slendy.Reset();
            finished = true;
        }
    }
}