using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        [SerializeField] private GameObject openDoorText;
        [SerializeField] private GameObject pickupFlashlightText;
        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject toggleFlashlightText;
        [SerializeField] private GameObject openChestText;
        [SerializeField] private GameObject quitPanel;
        [SerializeField] private GameObject livingRoomNote;
        [SerializeField] private GameObject keyRoomNote;
        [SerializeField] private GameObject readNoteText;

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
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<PlayerLook>()
                        .ResumeLook();
                    quitPanelActive = false;
                }
                else
                {
                    quitPanelActive = true;
                    quitPanel.SetActive(true);
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<PlayerLook>()
                        .StopLook();
                    Cursor.lockState = CursorLockMode.None;
                }
            }

            if (showingNote)
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("CLICKIN!");
                    DeactivateLivingRoomNote();
                    DeactivateKeyRoomNote();
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

        public void ShowOpenDoorText()
        {
            openDoorText.SetActive(true);
        }

        public void ShowFlashlightTutorialText()
        {
            toggleFlashlightText.SetActive(true);
            Invoke("HideFlashlightTutorialText", 3f);
        }

        public void HideFlashlightTutorialText()
        {
            toggleFlashlightText.SetActive(false);
        }

        public void HideOpenDoorText()
        {
            openDoorText.SetActive(false);
        }

        public void ShowPickUpObjectText(string objectName)
        {
            pickupFlashlightText.SetActive(true);
            pickupFlashlightText.GetComponent<TextMeshProUGUI>().text = "Press 'Q' to pick up " + objectName;
        }

        public void HidePickUpObjectText()
        {
            pickupFlashlightText.SetActive(false);
        }

        public void ShowOpenChestText()
        {
            openChestText.SetActive(true);
        }

        public void HideOpenChestText()
        {
            openChestText.SetActive(false);
        }

        public void ActivateFlashlightInventory()
        {
            inventory.SetActive(true);
            inventory.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void ActivateRustedKeyInventory()
        {
            inventory.SetActive(true);
            inventory.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void DeactivateRustedKeyInventory()
        {
            inventory.SetActive(false);
            inventory.transform.GetChild(1).gameObject.SetActive(false);
        }

        public void ShowLivingRoomNote()
        {
            livingRoomNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void ShowKeyRoomNote()
        {
            keyRoomNote.SetActive(true);
            Invoke("SetShowingNoteToTrue", 1f);
        }

        public void DeactivateLivingRoomNote()
        {
            livingRoomNote.SetActive(false);
        }

        private void DeactivateKeyRoomNote()
        {
            keyRoomNote.SetActive(false);
        }

        public void ShowLeftClickForNoteText()
        {
            readNoteText.SetActive(true);
        }

        public void HideLeftClickForNoteText()
        {
            readNoteText.SetActive(false);
        }

        private void SetShowingNoteToTrue()
        {
            showingNote = true;
        }
    }
}