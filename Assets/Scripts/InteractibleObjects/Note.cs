﻿using System;
using Inventory;
using UI;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum NoteName
    {
        LIVINGROOMNOTE,
        LIVINGROOMNOTESLENDERDRAWING,
        KEYROOMNOTE,
        BEDROOMNOTE,
        DUNGEONNOTEENTRANCE,
        DUNGEONNOTESKELETON
    }

    public Action<NoteName> noteRead;

    [SerializeField] private NoteName noteName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLook.instance.StartPollingForPLayerLookNote();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLook.instance.StopPollingForPLayerLookNote();
        }
    }

    public void ShowNote()
    {
        if (noteRead != null)
        {
            noteRead.Invoke(noteName);
        }

        switch (noteName)
        {
            case NoteName.LIVINGROOMNOTE:
                UIController.instance.ShowLivingRoomNote();
                return;
            case NoteName.LIVINGROOMNOTESLENDERDRAWING:
                UIController.instance.ShowLivingRoomNoteSlenderDrawing();
                return;
            case NoteName.KEYROOMNOTE:
                UIController.instance.ShowKeyRoomNote();
                return;
            case NoteName.BEDROOMNOTE:
                UIController.instance.ShowBedRoomNote();
                return;
            case NoteName.DUNGEONNOTEENTRANCE:
                UIController.instance.ShowDungeonEntranceNote();
                return;
            case NoteName.DUNGEONNOTESKELETON:
                UIController.instance.ShowDungeonSkeletonNote();
                return;
        }
    }
}