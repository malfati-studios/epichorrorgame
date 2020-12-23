using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VolumeController : MonoBehaviour
{
    private PostProcessVolume volume;
    private Grain grain;

    private FloatLerper lerper;

    private bool playingFX;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out grain);
    }

    public void StartChromaticAberrationFX()
    {
        lerper = new FloatLerper(2f, AbstractLerper<float>.SMOOTH_TYPE.EASE_OUT);
        lerper.SetValues(0f, 0f, true);
        playingFX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // StartChromaticAberrationFX();
            volume.weight = 0;    
        }

        if (playingFX)
        {
            Debug.Log("running? " + lerper.CurrentValue);
            lerper.Update();

            grain.intensity = new FloatParameter {value = lerper.CurrentValue};
            grain.active = true;
            grain.enabled = new BoolParameter {value = true};

            if (lerper.Reached)
            {
                playingFX = false;
            }
        }
    }
}