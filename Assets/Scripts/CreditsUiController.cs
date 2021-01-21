using TMPro;
using UnityEngine;

public class CreditsUiController : MonoBehaviour
{
    [SerializeField] private GameObject[] texts;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject nextButton;

    private int textsIndex;

    private void Start()
    {
        texts[textsIndex].SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        SceneController.instance.LoadMainMenu();
    }

    public void OnNextButtonPressed()
    {
        if (textsIndex < texts.Length)
        {
            texts[textsIndex].SetActive(false);
            textsIndex++;
            texts[textsIndex].SetActive(true);
        }

        if (textsIndex == texts.Length - 1)
        {
            nextButton.SetActive(false);
        }
    }
}