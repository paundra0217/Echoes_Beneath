using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton : MonoBehaviour
{
    GeneratorPuzzle generatorPuzzle;
    GeneratorButton generatorButton;

    private void Start()
    {
        generatorPuzzle = GetComponentInParent<GeneratorPuzzle>();
        generatorButton = GetComponent<GeneratorButton>();
    }

    private void OnMouseDown()
    {

        if (generatorPuzzle.Benar)
        {
            generatorPuzzle.WinGames();
            FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>().GeneratorObjectiveClear();
        }
        else
        {
            generatorPuzzle.Losegames();
        }
    }

}
