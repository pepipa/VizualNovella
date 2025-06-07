using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image backgroundImage;

    public Sprite blackBg;
    public Sprite chapter1Bg;
    public Sprite chapter2Bg;
    public Sprite chapter2BgCRN;
    public Sprite chapter2BgCRH;

    public void SetBackground(string name)
    {
        switch (name)
        {
            case "black":
                backgroundImage.sprite = blackBg;
                break;
            case "chapter1":
                backgroundImage.sprite = chapter1Bg;
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
            default:
                Debug.LogWarning("��� ������ ����: " + name);
                break;
        }
    }
}
