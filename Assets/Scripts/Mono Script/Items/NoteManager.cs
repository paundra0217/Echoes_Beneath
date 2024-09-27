using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] Gambar;
    [SerializeField] private Image image;


    public void SetGambar(int index)
    {
        image.sprite = Gambar[index];
    }



}
