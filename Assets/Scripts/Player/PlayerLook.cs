using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;
    public float mouseSensitivity = 2f;

    public Transform playerBody;

    float xRotation = 0f;

    private bool look = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
}