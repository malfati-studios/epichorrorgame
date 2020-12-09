﻿using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioSource openDoorSound;
    [SerializeField] private AudioSource closeDoorSound;
    [SerializeField] private AudioSource grabKeySound;
    [SerializeField] private AudioSource flashlightSound;
    [SerializeField] private AudioSource[] footstepsSounds;


    // Start is called before the first frame update
    void Awake()
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

    public void PlayOpenDoorSound()
    {
        openDoorSound.Play();
    }

    public void PlayCloseDoorSound()
    {
        closeDoorSound.Play();
    }

    public void PlayFootstepSound()
    {
        int index = Random.Range(0, footstepsSounds.Length);
        footstepsSounds[index].Play();
    }

    public void PlayGrabKeySound()
    {
        grabKeySound.Play();
    }

    public void PlayFlashlightSound()
    {
        flashlightSound.Play();
    }
}