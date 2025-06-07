using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;

    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;

    private void Start()
    {
        if (text != null)
            text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
            text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null)
            text.color = normalColor;
    }
}
