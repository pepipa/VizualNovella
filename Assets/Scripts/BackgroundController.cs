using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image backgroundImage;

    public Sprite blackBg;
    public Sprite chapterBg;

    public void SetBackground(string name)
    {
        switch (name)
        {
            case "black":
                backgroundImage.sprite = blackBg;
                break;
            case "chapter":
                backgroundImage.sprite = chapterBg;
                break;
            default:
                Debug.LogWarning("Нет такого фона: " + name);
                break;
        }
    }
}
