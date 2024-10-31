using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RDCT.PlayerController;

public class GeneratorPuzzle : PuzzleBase
{
    [Header("Kunci Jawaban")]
    [SerializeField] private bool Kunci1;
    [SerializeField] private bool Kunci2;
    [SerializeField] private bool Kunci3;
    [SerializeField] private bool Kunci4;
    [Header("Generator SetUp")]
    [SerializeField] private Lever Lever1;
    [SerializeField] private Lever Lever2;
    [SerializeField] private Lever Lever3;
    [SerializeField] private Lever Lever4;
    [SerializeField] private GeneratorButton generatorButton;

    [SerializeField] MeshRenderer gantiwarna;
    [SerializeField] UnityEvent KelarMinigamesEvent;
    [SerializeField] private Material hijau;

    GeneratorPuzzle generatorPuzzle;
    [SerializeField] BoxCollider GeneratorCollider;
    public PuzzleInteract puzzleInteract;
    public bool Benar;

    private void Start()
    {
        puzzleInteract = GetComponent<PuzzleInteract>();
        generatorPuzzle = GetComponent<GeneratorPuzzle>();
    }

    private void OnEnable()
    {
        Generatorenabled(true);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelMinigames();
        }

        if (Kunci1 == Lever1.Kunci)
        {
            if (Kunci2 == Lever2.Kunci)
            {
                if (Kunci3 == Lever3.Kunci)
                {
                    if (Kunci4 == Lever4.Kunci)
                    {
                        Benar = true;
                        return;
                    }
                }
            }
        }
        Benar = false;
    }

    public void WinGames()
    {
        Debug.Log("menang");
        KelarMinigamesEvent.Invoke();
        Generatorenabled(false);
        puzzleInteract.minigameskelar = true;
        gantiwarna.material = hijau;
        CancelMinigames();
    }

    private void Generatorenabled(bool oke)
    {
        Lever1.enabled = oke;
        Lever2.enabled = oke;
        Lever3.enabled = oke;
        Lever4.enabled = oke;
        GeneratorCollider.enabled = !oke;
        generatorButton.enabled = oke;
    }

    public void Losegames()
    {
        Debug.Log("kalah");
        Generatorenabled(false);
        CancelMinigames();
    }

    public void CancelMinigames()
    {
        Generatorenabled(false);
        puzzleInteract.TutorText.SetActive(false);
        FindObjectOfType<PlayerMotor>().GetComponent<PlayerMotor>().ChangeState(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        puzzleInteract.virtualCamera.gameObject.SetActive(false);
        generatorPuzzle.enabled = false;
        FindObjectOfType<InputManager>().GetComponent<InputManager>().enabled = true;        

    }

    

}
