using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance;
    public Transform[] slots;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckVictory()
    {
        int totalSlots = slots.Length;
        int correctCount = 0;

        foreach (Transform slot in slots)
        {   
            if (slot.childCount == 0)
            {
                return; 
            }

            GameObject placedPiece = slot.GetChild(0).gameObject;
            string expectedName = slot.GetComponent<PuzzleSlot>().correctPieceName;

            Debug.Log(slot.name + placedPiece.name + expectedName);

            if (placedPiece.name != expectedName)
            {
                return; 
            }

            correctCount++;
        }

        if (correctCount == totalSlots)
        {
            Debug.Log("Puzzle completed!");
            SceneManager.LoadScene("Chapter2PuzzleContinue");
        }
    }
}