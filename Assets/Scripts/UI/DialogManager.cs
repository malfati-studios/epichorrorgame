﻿using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private float timePerCharacter = 0.05f;

    private string[] firstDialog = new[]
    {
        "I have no option but to enter this cabin if I don't want to spend the night in the woods",
        "I think there's nobody home"
    };

    private string[] getFlashLightDialog = new[]
        {"A flashlight might be useful"};

    private string[] getRustedKeyDialog = new[]
        {"Here's the key!"};

    private string[] allTorchesLitDialog = new[]
        {"I think that's all of them. Let's see if anything changed"};

    private string[] currentDialog;
    private int currentDialogIndex;

    public void StartingDialog()
    {
        StartDialog(firstDialog);
    }

    public void GetFlashLightDialog()
    {
        StartDialog(getFlashLightDialog);
    }

    public void GetRustedKeyDialog()
    {
        StartDialog(getRustedKeyDialog);
    }

    public void ShowDialog(string[] dialog)
    {
        StartDialog(dialog);
    }

    private void StartDialog(string[] dialog)
    {
        textWriter.AddWriter(text, dialog[0], timePerCharacter, true);
        currentDialog = dialog;
        currentDialogIndex = 0;
    }

    private void OnCompleteText(bool obj)
    {
        currentDialogIndex++;
        if (currentDialogIndex < currentDialog.Length)
        {
            Invoke("CleanUpTextDelayAndShowNext", 3f);
        }
        else
        {
            Invoke("CleanUpTextDelay", 3f);
        }
    }

    private void CleanUpTextDelay()
    {
        text.text = "";
    }

    private void CleanUpTextDelayAndShowNext()
    {
        text.text = "";
        textWriter.AddWriter(text, currentDialog[currentDialogIndex], timePerCharacter, true);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        textWriter.onCompleteText += OnCompleteText;
    }

    public void ShowAllTorchesLitDialog()
    {
       ShowDialog(allTorchesLitDialog);
    }
}