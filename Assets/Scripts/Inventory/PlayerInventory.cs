﻿using System;
using Events;
using UI;
using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory instance;
        public Action<EventName> inventoryEvents;

        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject lighterPrefabPhysics;
        [SerializeField] private Transform throwingAltitude;

        private bool hasFlashlight;
        private bool hasRustedKey;
        private bool activeFlashLight;
        private bool hasLighter;
        private bool canThrowLighter;


        private bool pollingPlayerLookNote;
        private bool pollingPlayerTorch;

        private void Awake()
        {
            instance = this;
        }

        public void PickUpFlashlight()
        {
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
            hasRustedKey = true;
            inventoryEvents.Invoke(EventName.PICK_UP_RUSTED_KEY);
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

            if (hasLighter)
            {
                if (pollingPlayerTorch)
                {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10f))
                    {
                        if (hit.transform.CompareTag("Torch") &&
                            !hit.transform.gameObject.GetComponent<Torch>().IsLit())
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

            if (canThrowLighter && Input.GetMouseButtonDown(1))
            {
                mainCamera.transform.GetChild(1).gameObject.SetActive(false);
                GameObject throwableLighter =
                    Instantiate(lighterPrefabPhysics, new Vector3(throwingAltitude.position.x, throwingAltitude.position.y -0.1f, throwingAltitude.position. z), Quaternion.identity);
                throwableLighter.GetComponent<Rigidbody>().AddForce(throwingAltitude.forward * 5f, ForceMode.Impulse);
            }
        }

        public void PickUpLighter()
        {
            UIController.instance.ShowTimedMessage("Press Left mouse click to light up torches", 3f);
            hasLighter = true;
            hasFlashlight = false;
            mainCamera.transform.GetChild(1).gameObject.SetActive(true);
            mainCamera.transform.GetChild(0).gameObject.SetActive(false);
            inventoryEvents.Invoke(EventName.PICK_UP_LIGHTER);
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

        public void AllowThrowLighter()
        {
            canThrowLighter = true;
        }
    }
}