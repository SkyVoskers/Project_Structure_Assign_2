using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private Transform PlayerCameraPivot; // Reference to the PlayerCameraPivot empty GameObject
    [SerializeField] private CharacterController Controller;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpF;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f;

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);
        if (Controller.isGrounded)
        {
            Velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Velocity.y = JumpF;
            }
        }
        else
        {
            Velocity.y -= Gravity * -2f * Time.deltaTime;
        }
        Controller.Move(MoveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // Rotate the camera pivot for vertical rotation (pitch)
        PlayerCameraPivot.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        // Rotate the entire player object for horizontal rotation (yaw)
        transform.Rotate(Vector3.up * PlayerMouseInput.x * Sensitivity);
    }



}
