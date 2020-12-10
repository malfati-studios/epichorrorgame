using System.Collections;
using UnityEngine;

public class LevelLight : MonoBehaviour
{
    [SerializeField] private Material litMaterial;
    [SerializeField] private Material unlitMaterial;

    private Light light;
    private MeshRenderer meshRenderer;
    private bool flickering;
    private Coroutine flickerCoroutine;
    private bool on;

    void Start()
    {
        light = transform.GetChild(0).GetComponent<Light>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
        on = true;
    }

    public void TurnOn()
    {
        if (flickering)
        {
            flickering = false;
            StopCoroutine(flickerCoroutine);
        }

        InternalTurnOn();
    }

    public void TurnOff()
    {
        if (flickering)
        {
            flickering = false;
            StopCoroutine(flickerCoroutine);
        }

        InternalTurnOff();
    }

    public void StartFlickering(float minWaitTime, float maxWaitTime)
    {
        flickering = true;
        flickerCoroutine = StartCoroutine(Flicker(minWaitTime, maxWaitTime));
    }

    private IEnumerator Flicker(float minWaitTime, float maxWaitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            if (on)
            {
                InternalTurnOff();
            }
            else
            {
                InternalTurnOn();
            }
        }
    }

    private void InternalTurnOn()
    {
        meshRenderer.material = litMaterial;
        light.enabled = true;
        on = true;
    }

    private void InternalTurnOff()
    {
        meshRenderer.material = unlitMaterial;
        light.enabled = false;
        on = false;
    }
}