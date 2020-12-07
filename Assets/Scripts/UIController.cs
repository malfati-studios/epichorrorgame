using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject openDoorText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowOpenDoorText()
    {
        openDoorText.SetActive(true);
    }

    public void HideOpenDoorText()
    {
        openDoorText.SetActive(false);
    }

// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}