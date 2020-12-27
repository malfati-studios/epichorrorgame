using System;
using Events;
using UI;
using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public Action<EventName> inventoryEvents;

        [SerializeField] private GameObject mainCamera;

        private bool hasFlashlight;
        private bool hasRustedKey;
        private bool activeFlashLight;

        private bool pollingPlayerLookNote;
        private bool pollingPlayerTorch;

        public void PickUpFlashlight()
        {
            UIController.instance.ActivateFlashlightInventory();
            UIController.instance.ShowTimedMessage("Press Left mouse click to toggle flashlight", 3f);
            mainCamera.transform.GetChild(0).gameObject.SetActive(true);
            hasFlashlight = true;
            inventoryEvents.Invoke(EventName.PICK_UP_FLASHLIGHT);
        }

        public bool HasFlashlight()
        {
            return hasFlashlight;
        }

        public bool HasRustedKey()
        {
            return hasRustedKey;
        }

        public void PickUpRustedKey()
        {
            UIController.instance.ActivateRustedKeyInventory();
            hasRustedKey = true;
            inventoryEvents.Invoke(EventName.PICK_UP_RUSTED_KEY);
        }

        public void UseRustedKey()
        {
            UIController.instance.DeactivateRustedKeyInventory();
            hasRustedKey = false;
        }

        private void Update()
        {
            if (hasFlashlight)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    activeFlashLight = !activeFlashLight;
                    mainCamera.transform.GetChild(0).GetChild(0).gameObject.SetActive(activeFlashLight);
                    AudioController.instance.PlayFlashlightSound();
                }
            }

            if (pollingPlayerLookNote)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (hit.transform.CompareTag("Note"))
                    {
                        UIController.instance.ShowMessage("Left click to read note");
                        if (Input.GetMouseButtonDown(0))
                        {
                            hit.transform.GetComponent<Note>().ShowNote();
                        }
                    }
                    else
                    {
                        UIController.instance.HideLeftClickForNoteText();
                    }
                }
            }
            
            if (pollingPlayerTorch)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (hit.transform.CompareTag("Torch"))
                    {
                        UIController.instance.ShowLightUpTorchText();
                        if (Input.GetMouseButtonDown(0))
                        {
                            hit.transform.GetComponent<Torch>().TurnOn();
                        }
                    }
                    else
                    {
                        UIController.instance.HideMessage();
                    }
                }
            }
        }

        public void OpenedChest()
        {
        }

        public void StartPollingForPLayerLookNote()
        {
            pollingPlayerLookNote = true;
        }

        public void StopPollingForPLayerLookNote()
        {
            UIController.instance.HideLeftClickForNoteText();
            pollingPlayerLookNote = false;
        }

        public void StartPollingForTorch()
        {
            pollingPlayerTorch = true;
        }

        public void StopPollingForTorch()
        {
            UIController.instance.HideLightUpTorchText();
            pollingPlayerTorch = false;
        }
    }
}