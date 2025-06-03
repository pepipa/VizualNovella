using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public List<Sprite> emotions;
    public string characterName;
    private Vector3 defaultScale;
    public string currentEmotionVariable;
    public Vector3 DefaultScale => defaultScale;
    private void OnEnable()
    {
        defaultScale = transform.localScale;
    }
    public void ChangeEmotions(int currentExpression)
    {
        GetComponent<Image>().sprite = emotions[currentExpression];
    }

    public void ChangeScale(float multiplier)
    {
        transform.localScale *= multiplier;
    }

    public void ResetScale()
    {
        transform.localScale = defaultScale;
    }
}
