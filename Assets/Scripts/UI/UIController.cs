using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        [SerializeField] private GameObject torchesText;
        [SerializeField] private GameObject defaultMessage;
        [SerializeField] private GameObject quitPanel;
        [SerializeField] private GameObject livingRoomNote;
        [SerializeField] private GameObject livingRoomNoteSlenderDrawing;
        [SerializeField] private GameObject bedRoomNote;
        [SerializeField] private GameObject keyRoomNote;
        [SerializeField] private GameObject dungeonEntranceNote;
        [SerializeField] private GameObject dungeonSkeletonNote;


        private bool quitPanelActive;
        private bool showingNote;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (quitPanelActive)
                {
                    quitPanel.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    PlayerLook.instance.ResumeLook();
                    quitPanelActive = false;
                }
                else
                {
                    quitPanelActive = true;
                    quitPanel.SetActive(true);
                    PlayerLook.instance.StopLook();
                    Cursor.lockState = CursorLockMode.None;
                }
            }

            if (showingNote)
            {
                if (Input.GetMouseButton(0))
                {
                    DeactivateNotes();
                    showingNote = false;
                }
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }

        public void ShowMessage(string message)
        {
            defaultMessage.SetActive(true);
            defaultMessage.GetComponent<TextMeshProUGUI>().text = message;
        }

        public void ShowTimedMessage(string message, float seconds)
        {
            defaultMessage.SetActive(true);
            defaultMessage.GetComponent<TextMeshProUGUI>().text = message;
            Invoke("HideMessage", seconds);
        }

        public void HideMessage()
        {
            defaultMessage.SetActive(false);
        }

        public void ShowPickUpObjectText(string objectName)
        {
            defaultMessage.SetActive(true);
            defaultMessage.GetComponent<TextMeshProUGUI>().text = "Press 'Q' to pick up " + objectName;
        }

        public void ShowLivingRoomNote()
        {
            livingRoomNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowLivingRoomNoteSlenderDrawing()
        {
            livingRoomNoteSlenderDrawing.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowBedRoomNote()
        {
            bedRoomNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowKeyRoomNote()
        {
            keyRoomNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowDungeonEntranceNote()
        {
            dungeonEntranceNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowDungeonSkeletonNote()
        {
            dungeonSkeletonNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        private void DeactivateNotes()
        {
            livingRoomNote.SetActive(false);
            livingRoomNoteSlenderDrawing.SetActive(false);
            bedRoomNote.SetActive(false);
            keyRoomNote.SetActive(false);
            dungeonEntranceNote.SetActive(false);
            dungeonSkeletonNote.SetActive(false);
        }

        public void ShowLeftClickForNoteText()
        {
            defaultMessage.SetActive(true);
            defaultMessage.GetComponent<TextMeshProUGUI>().text = "Left click to read note";
        }

        public void HideLeftClickForNoteText()
        {
            defaultMessage.SetActive(false);
        }

        private void SetShowingNoteToTrue()
        {
            showingNote = true;
        }

        public void ShowLightUpTorchText()
        {
            defaultMessage.SetActive(true);
            defaultMessage.GetComponent<TextMeshProUGUI>().text = "Left click to light up torch";
        }

        public void HideLightUpTorchText()
        {
            defaultMessage.SetActive(false);
        }

        public void ShowTorchesUI()
        {
            torchesText.SetActive(true);
        }

        public void NotifyTorchLit(int lit)
        {
            torchesText.GetComponent<TextMeshProUGUI>().text = lit + "/10 Torches Lit";
        }

        public void HideLitTorchesUI()
        {
            torchesText.SetActive(false);
        }
    }
}