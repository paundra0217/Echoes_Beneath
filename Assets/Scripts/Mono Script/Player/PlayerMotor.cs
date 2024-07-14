using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RDCT.PlayerController
{


public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    //Stats
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float walkSpeed;
    private float jumpPower;
    private float gravity;
    private float lookSpeed;
    private float lookXLimit;
    private float defaultHeight;
    private float crouchHeight;
    private float crouchSpeed;

    //other
    private bool IsGrounded;
    private bool IsCrouch;
    Vector3 movedirection = Vector3.zero;

    //Camera
    [SerializeField] private Camera _cam;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        walkSpeed = _stats.walkSpeed;
        jumpPower = _stats.jumpPower;
        gravity = _stats.gravity;
        lookSpeed = _stats.lookSpeed;
        lookXLimit = _stats.lookXLimit;
        defaultHeight = _stats.defaultHeight;
        crouchHeight = _stats.crouchHeight;
        crouchSpeed = _stats.crouchSpeed;
    }


    #region Player_Movement
    //Player Movement
    public void ProcessMove(Vector2 input)
    {
        //Movement Input
        movedirection.x = input.x;
        movedirection.z = input.y;

        controller.Move(transform.TransformDirection(movedirection) * walkSpeed * Time.deltaTime);
        
        //apply Gravity
        playerVelocity.y -= gravity * Time.deltaTime;

        //Set Fall Speed Limit
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);



    }

    //Player Jump
    public void Jump()
    {
        //Debug.Log("masuk");
        if (controller.isGrounded)
        {
            //Debug.Log("lompat");
            playerVelocity.y = jumpPower;
        }

    }

    //Player Crouch
    public void Crouch()
    {
        //Toggle Crouch
        IsCrouch = !IsCrouch;

        if (IsCrouch)
        {
            controller.height = crouchHeight;
            walkSpeed = crouchSpeed;
        }
        else
        {
            controller.height = defaultHeight;
            walkSpeed = _stats.walkSpeed;
        }


    }

    #endregion

    #region Camera

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for up and down
        xRotation -= (mouseY * Time.deltaTime) * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);

        _cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * lookSpeed);
    }

    #endregion


}

}

