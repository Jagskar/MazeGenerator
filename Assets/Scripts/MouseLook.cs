using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;

    private float rotX = 0f;
    private float rotY = 0f;

    public float sensitivity = 100f;

    public Transform robotBody;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        robotBody.Rotate(Vector3.up * mouseX);
        
    }
}
