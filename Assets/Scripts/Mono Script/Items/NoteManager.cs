using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteManager : MonoBehaviour
{
    [SerializeField] private NoteStruct[] Gambar;
    [SerializeField] private Image imagekiri;
    [SerializeField] private Image imagekanan;
    [SerializeField] private Sprite GambarLocked;

    int indexHalaman = 0;
    int index = 0;
    int panjangArray;
    private void Start()
    {
        panjangArray = Gambar.Length /2;
        imagekiri.sprite = GambarLocked;
        imagekanan.sprite = GambarLocked;
    }

    public void UnlockPage(int indexUnlock)
    {
        Gambar[indexUnlock].UnlocK = true;
        UpdatePage();
    }

    public void SetGambar()
    {
        
    }

    public void NextPage()
    {
        if(indexHalaman + 1 == panjangArray)
        {
            indexHalaman = 0;
            index = 0;
        }
        else
        {
            indexHalaman++;
            index += 2;
        }
        UpdatePage();
    }

    public void PrevPage()
    {
        if (indexHalaman  == 0)
        {
            indexHalaman = panjangArray -1;
            index = panjangArray - 2;
        }
        else
        {
            indexHalaman--;
            index -= 2;
        }
        UpdatePage();
    }

    public void UpdatePage()
    {
        if(Gambar[index].UnlocK == false)
        {
            imagekiri.sprite = GambarLocked;
        }
        else
        {
            imagekiri.sprite = Gambar[index].Page;
        }

        if(Gambar[index + 1].UnlocK == false)
        {
            imagekanan.sprite = GambarLocked;
        }
        else
        {
            imagekanan.sprite = Gambar[index + 1].Page;
        }
    }

}

[System.Serializable]
public class NoteStruct
{
    public Sprite Page;
    public bool UnlocK;

}
