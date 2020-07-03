using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sawmill : Building
{
    private List<Tree> _orderedTrees;
    private int _woodAmount;

    private void OnDestroy()
    {
        Tree.OnTreeEmpty -= OnTreeEmptied;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        Tree.OnTreeEmpty += OnTreeEmptied;
        _orderedTrees = LandGenerator.Instance.Trees.OrderBy(x => (MainWaypoint.transform.position - x.transform.position).sqrMagnitude).ToList();
    }

    public Tree GetClosestTree()
    {
        if(_orderedTrees == null)
        {
            return null;
        }            
        return _orderedTrees.First();
    }

    public Tree GetClosestFreeTree()
    {
        if(_orderedTrees == null)
        {
            return null;
        }
        return _orderedTrees.Where(x => !x.occupied).First();
    }

    private void OnTreeEmptied(Tree t)
    {
        _orderedTrees.Remove(t);
    }

    public void AddWood(int amount)
    {
        _woodAmount += amount;
    }
}
