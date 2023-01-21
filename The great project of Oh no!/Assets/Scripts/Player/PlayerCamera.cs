using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform playerRotate;

    private float _rotationX = 0f;

    public Settings settings;

    [SerializeField]
    private float height;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }

    private void Update()
    {
        if(GetComponent<Camera>().fieldOfView != settings.FOV)
            GetComponent<Camera>().fieldOfView = settings.FOV;
        if(mouseSensitivity != settings.mouseSensitivity)
            mouseSensitivity = settings.mouseSensitivity;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        playerRotate.Rotate(Vector3.up * mouseX);
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
