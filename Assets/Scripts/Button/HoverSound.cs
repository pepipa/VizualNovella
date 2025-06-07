using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSound : MonoBehaviour
{
    public AudioClip clickSound;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        if (SFXManager.Instance != null && clickSound != null)
        {
            SFXManager.Instance.PlaySFX(clickSound);
        }
        else
        {
            Debug.LogWarning("SFXManager.Instance or clickSound is missing!");
        }
    }
}