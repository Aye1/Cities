using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    public float woodAmount = 100;
    public bool occupied = false;

    public delegate void TreeEvent(Tree t);
    public static TreeEvent OnTreeEmpty;


    private Vector3 _initialScale;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    public void CutWood(int amount)
    {
        woodAmount--;
        // Reset scale, in case we are cutting woods several times in one second
        transform.DOKill();
        transform.localScale = _initialScale;
        transform.DOPunchScale(Vector3.one * 0.03f, 1.0f);
       
        if(woodAmount <= 0)
        {
            DestroyTree();
        }
    }

    private void DestroyTree()
    {
        OnTreeEmpty?.Invoke(this);
        transform.DOKill();
        Destroy(gameObject);
    }
}
