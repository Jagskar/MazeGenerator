using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    float x;
    float z;

    Vector3 movement;
    Vector3 velocity;

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z;
        controller.Move(movement * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //GetComponent<RigidBody>().velocity = movement * speed * Time.deltaTime;
    }
}
