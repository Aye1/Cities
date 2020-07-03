using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sawmill : Building
{
    private IEnumerable<GameObject> _orderedTrees;

    protected override void OnCreate()
    {
        base.OnCreate();
        _orderedTrees = LandGenerator.Instance.Trees.OrderBy(x => (MainWaypoint.transform.position - x.transform.position).sqrMagnitude);
    }

    public GameObject GetClosestTree()
    {
        if(_orderedTrees == null)
        {
            return null;
        }            
        return _orderedTrees.First();
    }
}
