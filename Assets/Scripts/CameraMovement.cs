using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject robot;

    void Start()
    {
        robot = gameObject;
    }

    void Update()
    {
        Vector2 delta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        float dim = sensitivity * smoothing;
        delta = Vector2.Scale(delta, new Vector2(dim, dim));
        smoothV.x = Mathf.Lerp(smoothV.x, delta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, delta.y, 1f / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(mouseLook.y, Vector3.right);
        robot.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, robot.transform.up);
    }
}
