using DefaultNamespace;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextWriter textWriter;

    private string[] firstDialog = new[]
        {"I'ts been a long time since I was last here. I better clean up", "Hope there's nothing scary"};


    void Start()
    {
        textWriter.onCompleteText += OnCompleteText;
        textWriter.AddWriter(text, firstDialog[0], .1f, true);
    }

    private void OnCompleteText(bool obj)
    {
        Invoke("CleanUpTextDelay", 3f);
    }

    private void CleanUpTextDelay()
    {
        text.text = "";
        textWriter.AddWriter(text, firstDialog[1], .1f, true);
    }
}