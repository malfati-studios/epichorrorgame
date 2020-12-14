using System;
using UnityEngine;

namespace Events
{
    public class SlendyInHallwayEvent : MonoBehaviour, IScaryEvent
    {
        private Slendy slendy;
        private Transform slendySpawnPoint;
        private Transform slendyEndPoint;

        private float eventDelay = 2f;
        private DateTime eventStartedTime;

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

        private void StartEvent()
        {
            LightsController.instance.FlickerHallChandelier();
            AudioController.instance.PlayFirstJumpscareSound();
            slendy.StartWalking(slendySpawnPoint.position, slendyEndPoint.position);
            Invoke("EndEvent", 3f);
        }

        private void EndEvent()
        {
            LightsController.instance.TurnOnHallChandelier();
            slendy.Reset();
        }
    }
}