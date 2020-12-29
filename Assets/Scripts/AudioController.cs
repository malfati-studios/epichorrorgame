using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioSource openDoorSound;
    [SerializeField] private AudioSource closeDoorSound;
    [SerializeField] private AudioSource grabKeySound;
    [SerializeField] private AudioSource flashlightSound;
    [SerializeField] private AudioSource firstJumpscareSound;
    [SerializeField] private AudioSource openChestSound;
    [SerializeField] private AudioSource typeWriterSound;
    [SerializeField] private AudioSource[] footstepsSounds;
    [SerializeField] private AudioSource ambience;
    [SerializeField] private AudioSource horrorAmbience;
    [SerializeField] private AudioSource electricitySound;
    [SerializeField] private AudioSource highPitchedSound;

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

    public void PlayFirstJumpscareSound()
    {
        firstJumpscareSound.Play();
    }
    
    public void PlayHighPitchedScarySound()
    {
        highPitchedSound.Play();
    }

    public void PLayOpenChestSound()
    {
        openChestSound.Play();
    }

    public void StartDialogSound()
    {
        typeWriterSound.Play();
    }

    public void StopDialogSound()
    {
        typeWriterSound.Stop();
    }

    public void PlayAmbience()
    {
        ambience.Play();
    }

    public void PlayHorrorAmbience()
    {
        horrorAmbience.Play();
    }

    public void FadeOutAmbience()
    {
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0));
    }

    public void PlayElectricitySound()
    {
        electricitySound.Play();
    }
    
    public void StopElectricitySound()
    {
        electricitySound.Stop();
    }

    public void StopAmbienceSound()
    {
        horrorAmbience.Stop();
    }
}