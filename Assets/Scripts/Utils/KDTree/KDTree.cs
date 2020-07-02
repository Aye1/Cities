using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class KDTree
{
    private int _nbDimensions;
    private KDNode _firstNode;

    public KDTree(int nbDimensions = 3)
    {
        _nbDimensions = nbDimensions;
    }

    public KDNode GetFirstNode()
    {
        return _firstNode;
    }

    public void Add(GameObject obj)
    {
        if(_firstNode == null)
        {
            _firstNode = CreateNode(obj, KDDimension.X);
        } else
        {
            Insert(obj, _firstNode);
        }
    }

    public void Add(IEnumerable<GameObject> objs)
    {
        foreach(GameObject obj in objs)
        {
            Add(obj);
        }
    }

    public GameObject FindNearestNeighbour(Vector3 position)
    {
        return null;
    }

    private KDNode CreateNode(GameObject obj, KDDimension splitDimension)
    {
        KDNode res = new KDNode();
        res.obj = obj;
        res.splitDimension = splitDimension;
        return res;
    }

    private void Insert(GameObject obj, KDNode currentNode)
    {
        if(IsLowerThan(obj, currentNode.obj, currentNode.splitDimension))
        {
            if(currentNode.leftChild == null)
            {
                currentNode.leftChild = CreateNode(obj, NextDimension(currentNode.splitDimension, _nbDimensions));
            } else
            {
                Insert(obj, currentNode.leftChild);
            }
        } else
        {
            if (currentNode.rightChild == null)
            {
                currentNode.rightChild = CreateNode(obj, NextDimension(currentNode.splitDimension, _nbDimensions));
            }
            else
            {
                Insert(obj, currentNode.rightChild);
            }
        }
    }

    private bool IsLowerThan(GameObject obj1, GameObject obj2, KDDimension splitDimension)
    {
        switch(splitDimension)
        {
            case KDDimension.X:
                return obj1.transform.position.x < obj2.transform.position.x;
            case KDDimension.Y:
                return obj1.transform.position.y < obj2.transform.position.y;
            case KDDimension.Z:
                return obj1.transform.position.z < obj2.transform.position.z;
        }
        return false;
    }

    public static KDDimension NextDimension(KDDimension currentDimension, int nbDimensions)
    {
        if(nbDimensions == 2)
        {
            return currentDimension == KDDimension.X ? KDDimension.Z : KDDimension.X;
        }
        return (KDDimension)((int)(currentDimension + 1) % Enum.GetNames(typeof(KDDimension)).Length);
    }
}
