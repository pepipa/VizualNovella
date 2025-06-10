using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image backgroundImage;

    public Sprite blackBg;
    public Sprite chapter1Bg;
    public Sprite chapter2nDBg;
    public Sprite chapter2Bg;
    public Sprite chapter2BgCRN;
    public Sprite chapter2BgCRH;
    public Sprite chapter2BgMRN;
    public Sprite chapter2BgMRH;
    public Sprite chapter2BgrMH;
    public Sprite chapter3Bg;
    public Sprite chapter3BgD;
    public Sprite chapter3BgDZ;
    public Sprite chapter3AF;
    public Sprite whiteBg;

    public void SetBackground(string name)
    {
        switch (name)
        {
            case "black":
                backgroundImage.sprite = blackBg;
                break;
            case "white":
                backgroundImage.sprite = whiteBg;
                break;
            case "chapter1":
                backgroundImage.sprite = chapter1Bg;
                break;
            case "chapter2_no_doors":
                backgroundImage.sprite = chapter2nDBg;
                break;
            case "chapter2":
                backgroundImage.sprite = chapter2Bg;
                break;
            case "chapter2_class_room_normal":
                backgroundImage.sprite = chapter2BgCRN;
                break;
            case "chapter2_class_room_horror":
                backgroundImage.sprite = chapter2BgCRH;
                break;
            case "chapter2_med_room_normal":
                backgroundImage.sprite = chapter2BgMRN;
                break;
            case "chapter2_med_room_horror":
                backgroundImage.sprite = chapter2BgMRH;
                break;
            case "chapter2_room_mother_horror":
                backgroundImage.sprite = chapter2BgrMH;
                break;
            case "chapter3":
                backgroundImage.sprite = chapter3Bg;
                break;
            case "chapter3_doors":
                backgroundImage.sprite = chapter3BgD;;
                break;
            case "chapter3_door_zoom":
                backgroundImage.sprite = chapter3BgDZ; ;
                break;
            case "chapter3_alina_final":
                backgroundImage.sprite = chapter3AF; ;
                break;

            default:
                Debug.LogWarning("Нет такого фона: " + name);
                break;
        }
    }
}
