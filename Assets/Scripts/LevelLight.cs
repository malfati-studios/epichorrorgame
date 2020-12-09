using System.Collections;
using UnityEngine;

public class LevelLight : MonoBehaviour
{
    private Light light;
    private bool flickering;
    private Coroutine flickerCoroutine;

    void Start()
    {
        light = transform.GetChild(0).GetComponent<Light>();
    }

    public void TurnOn()
    {
        if (flickering)
        {
            flickering = false;
            StopCoroutine(flickerCoroutine);
        }

        light.enabled = true;
    }

    public void TurnOff()
    {
        if (flickering)
        {
            flickering = false;
            StopCoroutine(flickerCoroutine);
        }

        light.enabled = false;
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
            light.enabled = !light.enabled;
        }
    }
}