using Inventory;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;
        [SerializeField] private IEvent hallwayLightEvent;
        private IEvent allLightsOffEvent;
        private IEvent slendyScream;
        private IEvent noMainDoorDialogEvent;
        private IEvent keyRoomMossDialogEvent;
        private IEvent dungeonDoorAppearEvent;
        private IEvent dungeonDoorAppearDialogEvent;
        private IEvent trappedDungeonDialogEvent;
        private IEvent skeletonDialogEvent;
        private IEvent exitingDungeonEvent;
        private IEvent litAllTorchesEvent;
        private IEvent slendyFinalChaseEvent;

        public void GameStarted()
        {
            DialogManager.instance.StartingDialog();
        }

        private void OnDoorEvent(DoorEvent doorEvent)
        {
            if (doorEvent.doorName.Equals("hallwayDoor") && doorEvent.action.Equals(DoorAction.OPEN_DOOR) &&
                doorEvent.eventCount == 1)
            {
                hallwayLightEvent.FireEvent();
            }
        }

        private void Start()
        {
            DoorsController.instance.doorsEvents += OnDoorEvent;
            GameObject.Find("KeyRoomNote").GetComponent<Note>().noteRead += OnKeyRoomNoteRead;
            PlayerInventory.instance.inventoryEvents += OnInventoryEvent;
            SetUpEvents();
        }

        private void OnKeyRoomNoteRead(Note.NoteName obj)
        {
            dungeonDoorAppearEvent.FireEvent();
            dungeonDoorAppearDialogEvent.FireEvent();
        }

        private void OnInventoryEvent(EventName eventName)
        {
            switch (eventName)
            {
                case EventName.PICK_UP_RUSTED_KEY:
                    DialogManager.instance.GetRustedKeyDialog();
                    return;
                case EventName.PICK_UP_FLASHLIGHT:
                    DialogManager.instance.GetFlashLightDialog();
                    allLightsOffEvent.FireEvent();
                    noMainDoorDialogEvent.FireEvent();
                    keyRoomMossDialogEvent.FireEvent();
                    return;
                case EventName.PICK_UP_LIGHTER:
                    return;
            }
        }

        private void SetUpEvents()
        {
            hallwayLightEvent = transform.GetChild(0).GetChild(0).GetComponent<IEvent>();
            hallwayLightEvent.SetUpEvent();

            allLightsOffEvent = transform.GetChild(0).GetChild(1).GetComponent<IEvent>();
            allLightsOffEvent.SetUpEvent();

            dungeonDoorAppearEvent = transform.GetChild(0).GetChild(2).GetComponent<IEvent>();
            dungeonDoorAppearEvent.SetUpEvent();

            noMainDoorDialogEvent = transform.GetChild(0).GetChild(3).GetComponent<IEvent>();
            noMainDoorDialogEvent.SetUpEvent();

            keyRoomMossDialogEvent = transform.GetChild(0).GetChild(4).GetComponent<IEvent>();
            keyRoomMossDialogEvent.SetUpEvent();

            dungeonDoorAppearDialogEvent = transform.GetChild(0).GetChild(5).GetComponent<IEvent>();
            dungeonDoorAppearDialogEvent.SetUpEvent();

            trappedDungeonDialogEvent = transform.GetChild(0).GetChild(6).GetComponent<IEvent>();
            trappedDungeonDialogEvent.SetUpEvent();

            skeletonDialogEvent = transform.GetChild(0).GetChild(7).GetComponent<IEvent>();
            skeletonDialogEvent.SetUpEvent();

            exitingDungeonEvent = transform.GetChild(0).GetChild(8).GetComponent<IEvent>();
            exitingDungeonEvent.SetUpEvent();

            slendyFinalChaseEvent = transform.GetChild(0).GetChild(9).GetComponent<IEvent>();
            exitingDungeonEvent.SetUpEvent();
        }

        public void NotifyEnteredDungeon()
        {
            trappedDungeonDialogEvent.FireEvent();
            skeletonDialogEvent.FireEvent();
        }

        public void NotifyLitAllTorches()
        {
            exitingDungeonEvent.FireEvent();
            trappedDungeonDialogEvent.DeactivateEvent();
            DialogManager.instance.ShowAllTorchesLitDialog();
            slendyFinalChaseEvent.FireEvent();
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}