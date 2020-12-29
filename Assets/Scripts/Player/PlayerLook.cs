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

    private void Awake()
    {
        instance = this;
    }

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

    // Update is called once per frame
    void Update()
    {
        if (!look) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ShakeCamera(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        PlayerLook.instance.ResetCameraPosAfter(timer + .05f);
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

    private void ResetCameraPosAfter(float timer)
    {
        Invoke("ResetCameraPos", timer);
    }

    private void ResetCameraPos()
    {
        transform.localPosition = initialPosition;
    }
}