using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RDCT.PlayerController
{


    public class InputManager : MonoBehaviour
    {
        public Player_Input inputActions;
        private Player_Input.On_FootActions on_Foot;

        private PlayerMotor motor;

        private void Awake()
        {
            inputActions = new Player_Input();
            on_Foot = inputActions.On_Foot;
            on_Foot.Run.performed           += ctx => motor.Running();
            on_Foot.Jump.performed          += ctx => motor.Jump();
            on_Foot.Crouch.performed        += ctx => motor.Crouch();
            on_Foot.Interact.performed      += ctx => motor.Interact();
            on_Foot.Inventory.performed     += ctx => motor.Inventory();
            on_Foot.FlashLight.performed    += ctx => motor.FlashLight();
            on_Foot.Journal.performed       += ctx => motor.Journal();
            on_Foot.Pause.performed         += ctx => motor.PauseGame();
        }

        private void Start()
        {
            motor = GetComponent<PlayerMotor>();
        }

        private void FixedUpdate()
        {
            motor.ProcessMove(on_Foot.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            motor.ProcessLook(on_Foot.Look.ReadValue<Vector2>());
        }
        private void OnEnable()
        {
            on_Foot.Enable();
        }

        private void OnDisable()
        {
            on_Foot.Disable();
        }

    }

}
