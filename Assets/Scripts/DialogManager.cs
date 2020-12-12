using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextWriter textWriter;

    private string[] firstDialog = new[]
        {"I'ts been a long time since I was last here. I better clean up", "Hope there's nothing scary"};

    private int dialogIndex;

    public void StartingDialog()
    {
        textWriter.AddWriter(text, firstDialog[0], .1f, true);
        dialogIndex++;
    }
    
    void Start()
    {
        textWriter.onCompleteText += OnCompleteText;
    }

    private void OnCompleteText(bool obj)
    {
        if (dialogIndex < firstDialog.Length)
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
        textWriter.AddWriter(text, firstDialog[dialogIndex], .1f, true);
        dialogIndex++;
    }

    private void Awake()
    {
        instance = this;
    }
}