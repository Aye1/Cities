using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GoUpAndFadeOut : MonoBehaviour
{
    public float moveAmount = 1.0f;
    public float duration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + moveAmount, duration);
        GetComponentInChildren<TextMeshProUGUI>().DOFade(0.0f, duration).OnComplete(() => Destroy(gameObject));
    }
}
