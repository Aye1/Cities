using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KDDimension { X, Y, Z };

public class KDNode
{
    public GameObject obj;
    public KDNode leftChild;
    public KDNode rightChild;
    public KDDimension splitDimension;
}
