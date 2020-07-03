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

    public void CutWood(int amount)
    {
        woodAmount--;
        transform.DOPunchScale(Vector3.one * 0.03f, 1.0f);
        if(woodAmount <= 0)
        {
            DestroyTree();
        }
    }

    private void DestroyTree()
    {
        OnTreeEmpty?.Invoke(this);
        Destroy(gameObject);
    }
}
