using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.PlayerController;

public class PipePuzzle : PuzzleBase
{
    [Header("Input Minigames")]
    [SerializeField] KeyCode RotatePipeInput;
    [SerializeField] KeyCode Back;
    [Header("Valve SetUp")]
    [SerializeField] GameObject Valve;
    PuzzleInteract puzzleInteract;
    PipePuzzle pipePuzzle;
    private int Point = 0;
    [SerializeField] int PointToWin;
    

    private void Start()
    {
        puzzleInteract = GetComponent<PuzzleInteract>();
        pipePuzzle = GetComponent<PipePuzzle>();
    }

    private void Update()
    {
        if (Input.GetKey(RotatePipeInput))
        {
            Debug.Log("monyet");
            RotatePipe();
        }
        if (Input.GetKeyDown(Back))
        {
            CancelMinigames();
        }

    }


    public void RotatePipe()
    {
        Point += 1;
        Valve.transform.Rotate(0f, -100f * Time.deltaTime, 0f);
        if(Point >= PointToWin)
        {
            FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>().PipeObjectiveClear();
            puzzleInteract.enabled = false;
            puzzleInteract.minigameskelar = true;
            CancelMinigames();
        }
    }

    public void CancelMinigames()
    {
        FindObjectOfType<PlayerMotor>().GetComponent<PlayerMotor>().ChangeState(false);
        puzzleInteract.virtualCamera.gameObject.SetActive(false);
        FindObjectOfType<InputManager>().GetComponent<InputManager>().enabled = true;
        pipePuzzle.enabled = false;

    }

    



}
