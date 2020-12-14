using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextWriter textWriter;

    private string[] firstDialog = new[]
        {"I'ts been a long time since I was last here", "Hope there is nothing scary"};

    private string[] getFlashLightDialog = new[]
        {"A flashlight might be useful"};

    private string[] getRustedKeyDialog = new[]
        {"I wonder what this is for"};

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

    private void StartDialog(string[] dialog)
    {
        textWriter.AddWriter(text, dialog[0], .1f, true);
        currentDialog = dialog;
        currentDialogIndex = 1;
    }

    private void OnCompleteText(bool obj)
    {
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
        textWriter.AddWriter(text, currentDialog[currentDialogIndex], .1f, true);
        currentDialogIndex++;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        textWriter.onCompleteText += OnCompleteText;
    }
}