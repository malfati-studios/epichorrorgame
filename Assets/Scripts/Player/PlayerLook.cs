using UI;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;
    public float mouseSensitivity = 2f;

    public Transform playerBody;

    float xRotation = 0f;

    private bool look = true;
    private Vector3 initialPosition;

    private bool hasLighter;
    private bool pollingPlayerLookNote;
    private bool pollingPlayerTorch;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialPosition = transform.localPosition;
    }

    public void ResumeLook()
    {
        look = true;
    }

    public void StopLook()
    {
        look = false;
    }

    public void NotifyPlayerHasLighter()
    {
        hasLighter = true;
    }

    public void StartPollingForPLayerLookNote()
    {
        pollingPlayerLookNote = true;
    }

    public void StopPollingForPLayerLookNote()
    {
        UIController.instance.HideLeftClickForNoteText();
        pollingPlayerLookNote = false;
    }

    public void StartPollingForTorch()
    {
        pollingPlayerTorch = true;
    }

    public void StopPollingForTorch()
    {
        UIController.instance.HideLightUpTorchText();
        pollingPlayerTorch = false;
    }

    public void ShakeCamera(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        ResetCameraPosAfter(timer + .05f);
        FunctionUpdater.Create(delegate()
        {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement =
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!look) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        ProcessNoteAndTorchPollings();
    }

    private void ProcessNoteAndTorchPollings()
    {
        if (pollingPlayerLookNote)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.transform.CompareTag("Note"))
                {
                    UIController.instance.ShowMessage("Left click to read note");
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.GetComponent<Note>().ShowNote();
                    }
                }
                else
                {
                    UIController.instance.HideLeftClickForNoteText();
                }
            }
        }

        if (hasLighter)
        {
            if (pollingPlayerTorch)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (hit.transform.CompareTag("Torch") &&
                        !hit.transform.gameObject.GetComponent<Torch>().IsLit())
                    {
                        UIController.instance.ShowLightUpTorchText();
                        if (Input.GetMouseButtonDown(0))
                        {
                            hit.transform.GetComponent<Torch>().TurnOn();
                        }
                    }
                    else
                    {
                        UIController.instance.HideMessage();
                    }
                }
            }
        }
    }

    private void ResetCameraPosAfter(float timer)
    {
        Invoke("ResetCameraPos", timer);
    }

    private void ResetCameraPos()
    {
        transform.localPosition = initialPosition;
    }
}