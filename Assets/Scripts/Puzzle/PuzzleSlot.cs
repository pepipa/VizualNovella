using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    public string correctPieceName;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        if (dropped.name == correctPieceName)
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            PuzzleGameManager.Instance.CheckVictory();
        }
        else
        {
            dropped.GetComponent<PuzzlePiece>().ResetPosition();
        }
    }
}