using System.Collections.Generic;
using Events;
using UI;
using UnityEngine;

public class TorchesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> torches;
    private int torchesOnCounter;
    public static TorchesController instance;

    public void NotifyTorchLit()
    {
        torchesOnCounter++;
        UIController.instance.NotifyTorchLit(torchesOnCounter);
        if (torchesOnCounter == torches.Count)
        {
            UIController.instance.HideLitTorchesUI();
            EventManager.instance.NotifyLitAllTorches();
        }
    }

    private void Awake()
    {
        instance = this;
    }
}