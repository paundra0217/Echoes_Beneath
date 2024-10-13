using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RDCT.Audio;
using Cinemachine;
using TMPro;


public enum PlayerState { InMinigames, normal }


public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    //Stats
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float walkSpeed;
    private float RunSpeed;
    private float jumpPower;
    private float gravity;
    private float lookSpeed;
    private float lookXLimit;
    private float defaultHeight;
    private float crouchHeight;
    private float crouchSpeed;

    //other
    public bool CanRun = false;
    private bool JournalPicked = false;
    private bool IsGrounded;
    private bool IsOpenInventory = false;
    private bool IsOpenJournal = false;
    private bool ToggleFlashLight = false;
    private bool IsRunning = false;
    private bool IsCrouch = false;
    private CapsuleCollider coll;
    [SerializeField] Flashlight senter;
    [SerializeField] GameObject InventoryUI;
    [SerializeField] GameObject JournalUI;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float JarakInteract;
    [SerializeField] GameObject Interacttxt;
    private Image invUI;
    ItemGrid inventoryGrid;
    Vector3 movedirection = Vector3.zero;
    RaycastHit hit;

    //Camera
    [SerializeField] private CinemachineVirtualCamera[] _cam;
    private int CameraIndex = 0;
    [SerializeField] private Animator anim;
    private float xRotation = 0f;
    // Player State

    public PlayerState playerState = PlayerState.normal;

    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventoryUI = FindAnyObjectByType<GridInteract>().gameObject;
        inventoryGrid = InventoryUI.GetComponent<ItemGrid>();
        invUI = InventoryUI.GetComponent<Image>();
        invUI.enabled = false;
        //InventoryUI.SetActive(false);
    }

    private void Update()
    {
        CheckInteract();
    }

    private void Awake()
        {
        //mindahin data SO ke variabel local
        walkSpeed = _stats.walkSpeed;
        RunSpeed = _stats.RunSpeed;
        jumpPower = _stats.jumpPower;
        gravity = _stats.gravity;
        lookSpeed = _stats.lookSpeed;
        lookXLimit = _stats.lookXLimit;
        defaultHeight = _stats.defaultHeight;
        crouchHeight = _stats.crouchHeight;
        crouchSpeed = _stats.crouchSpeed;

        try
        {
            AudioController.Instance.PlayBGM("SewerAmbiance");
        }
        catch
        {
            Debug.LogWarning("Audio controller is missing from scene.");
        }
    }
    
    public void PlayerSetUp()
    {
        lookSpeed = _stats.lookSpeed;
    }

    public PlayerState getPlayerstate()
    {
        return playerState;
    }

    #region Player_Movement & input
    //Player Movement
    public void ProcessMove(Vector2 input)
    {
        //Movement Input
        movedirection.x = input.x;
        movedirection.z = input.y;
        float move = Mathf.Max(Mathf.Abs(movedirection.x), Mathf.Abs(movedirection.z));
        //if (move > 0) audioSource.enabled = true;
        //else audioSource.enabled = false;
        anim.SetFloat("movement", move);

        controller.Move(transform.TransformDirection(movedirection) * walkSpeed * Time.deltaTime);
        
        //apply Gravity
        playerVelocity.y -= gravity * Time.deltaTime;

        //Set Fall Speed Limit
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        //Player Move
        controller.Move(playerVelocity * Time.deltaTime);



    }

    //PlayerRun
    public void Running()
    {
        if (!CanRun)
        {
            return;
        }

        //Buat toggle Lari
        IsRunning = !IsRunning;
        //kalo true pake RunSpeed, kalo false pake Walkspeed
        if (IsRunning)
        {
            walkSpeed = RunSpeed;
        }
        else
        {
            walkSpeed = _stats.walkSpeed;
        }

    }

    //Player Jump
    public void Jump()
    {
        //Kalo Grounded baru bisa lompat
        if (controller.isGrounded)
        {           
            playerVelocity.y = jumpPower;
        }

    }

    //Player Crouch
    public void Crouch()
    {
        //Toggle Crouch
        IsCrouch = !IsCrouch;
        anim.SetBool("Isjongkok", IsCrouch);
        if (IsCrouch)
        {
            //controller.height = crouchHeight;
            controller.center = new Vector3(0,-0.25f,0);
            controller.height = 1.5f;
            _cam[1].gameObject.SetActive(true);
            _cam[0].gameObject.SetActive(false);
            CameraIndex = 1;
            walkSpeed = crouchSpeed;
        }
        else
        {

            _cam[1].gameObject.SetActive(false);
            _cam[0].gameObject.SetActive(true);
            CameraIndex = 0;
            controller.center = new Vector3(0, 0, 0);
            controller.height = 2f;
            //controller.height = defaultHeight;
            walkSpeed = _stats.walkSpeed;
        }


    }
    //Player Interact
    public void Interact()
    {
        //Kalo Raycast gk nemu apa apa, gk ngapa ngapain
        if (hit.collider == null)
        {
            //Debug.Log("gk ada apa apa");
            return;
        }

        //Kalo ada objek, Function dalam Object dijalanin
        InteractObject interactObject = hit.collider.gameObject.GetComponent<InteractObject>();
        if (ToggleFlashLight)
        {
            //FlashLight();
        }
        
        interactObject.Interaction();

    }

    //Player Open/Close Inventory
    public void Inventory()
    {
        //toggle Open/Close Inventory
        IsOpenInventory = !IsOpenInventory;
        
        //InventoryUI.SetActive(IsOpenInventory);

        if (IsOpenInventory)
        {
            invUI.enabled = true;
            inventoryGrid.enableAllItem(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            invUI.enabled = false;
            inventoryGrid.enableAllItem(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        

    }

    //Player On/Off FlashLight
    public void FlashLight()
    {
        ToggleFlashLight = !ToggleFlashLight;
        AudioController.Instance.PlaySFX("Flashlight");
        if (ToggleFlashLight)
        {
            senter.FlashOn();
        }
        else
        {
            senter.FlashOff();
        }

    }
    // Player Open/Close Journal
    public void Journal()
    {
        if (!JournalPicked) return;
        Debug.Log("BukaJournal");
        //IsOpenJournal = !IsOpenJournal;
        if(JournalUI.activeSelf == true)
        {
            JournalUI.SetActive(false);
        }

    }
    //Player
    #endregion

    #region Camera

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for up and down
        xRotation -= (mouseY * Time.deltaTime) * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);

        _cam[CameraIndex].transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * lookSpeed);
    }

        #endregion

    #region Interact 
    
    public void CheckInteract()
    {
        Physics.Raycast(_cam[CameraIndex].transform.position, _cam[CameraIndex].transform.forward, out hit, JarakInteract, layerMask);
        if(hit.collider != null && playerState == PlayerState.normal)
        {
            Interacttxt.SetActive(true);
        }
        else
        {
            Interacttxt.SetActive(false);
        }

    }
    #endregion

    #region


    public void JournalPick()
    {
        JournalPicked = true;
    }

    public void ChangeState(bool oke)
    {
        if (oke)
        {
            playerState = PlayerState.InMinigames;
        }
        else
        {
            playerState = PlayerState.normal;
        }

    }
    #endregion
}



