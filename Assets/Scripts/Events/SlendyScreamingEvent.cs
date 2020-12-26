using UnityEngine;

namespace Events
{
    public class SlendyScreamingEvent : MonoBehaviour, IScaryEvent
    {
        private Slendy slendy;
        private Transform slendySpawnPoint;
        private bool finished;
        // Start is called before the first frame update

        private void OnTriggerEnter(Collider other)
        {
            if (!finished)
            {
                FireEvent();
            }
        }

   
        public void SetUpEvent()
        {
            slendy = GameObject.FindGameObjectWithTag("Slendy").GetComponent<Slendy>();
            slendySpawnPoint = transform.GetChild(0);
        }

        public void FireEvent()
        {
            slendy.Scream(slendySpawnPoint.position);
            finished = true;
        }

        public bool EventFinished()
        {
            return finished;
        }
    }
}
