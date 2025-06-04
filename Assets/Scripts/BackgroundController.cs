using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image backgroundImage;

    public Sprite blackBg;
    public Sprite whiteBg;
    public Sprite bolnicaBg;

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
            case "bolnica":
                backgroundImage.sprite = bolnicaBg;
                break;
            default:
                Debug.LogWarning("Нет такого фона: " + name);
                break;
        }
    }
}
