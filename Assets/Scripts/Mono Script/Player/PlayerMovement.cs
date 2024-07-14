using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RDCT.PlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public Camera playerCamera;
        [SerializeField] private PlayerStats _stats;
        //Stats
        private float walkSpeed;
        private float jumpPower;
        private float gravity;
        private float lookSpeed;
        private float lookXLimit;
        private float defaultHeight;
        private float crouchHeight;
        private float crouchSpeed;


        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private CharacterController characterController;

        private bool canMove = true;

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
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? (walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.C) && canMove)
            {
                characterController.height = crouchHeight;
                walkSpeed = crouchSpeed;

            }
            else
            {
                characterController.height = defaultHeight;
                walkSpeed = 6f;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }

}