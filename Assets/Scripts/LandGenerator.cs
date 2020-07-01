using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LandGenerator : MonoBehaviour
{
    public float size = 10.0f;
    [Range(0.0f, 1.0f)] public float density = 1.0f;
    [Range(0.0f, 1.0f)] public float noise = 0.5f;
    public float stepSize = 1.0f;
    public int numberBalances = 0;
    public List<GameObject> trees;

    private Dictionary<Vector2Int, GameObject> _createdTrees;
    private int numberSteps;

    // Start is called before the first frame update
    void Start()
    {
        numberSteps = (int)(size / stepSize);
        GenerateRandomTrees();
        for(int i = 0; i < numberBalances; i++)
        {
            BalanceTrees();
        }
    }

    public void GenerateRandomTrees()
    {
        _createdTrees = new Dictionary<Vector2Int, GameObject>();
        if(density == 0.0f)
        {
            return;
        }
        for (int i = 0; i < 2 * numberSteps; i++)
        {
            for (int j = 0; j < 2 * numberSteps; j++)
            {
                if(Alea.GetFloat(0.0f, 1.0f) <= density)
                {
                    CreateTree(new Vector2Int(i,j));
                }
            }
        }
    }

    private GameObject GenerateTree(Vector3 position)
    {
        int id = Alea.GetInt(0, trees.Count);
        GameObject tree = Instantiate(trees[id], Vector3.zero, Quaternion.identity, transform);
        tree.transform.localPosition = position;
        tree.transform.localScale = Vector3.one * 0.3f;
        return tree;
    }

    public void BalanceTrees()
    {
        List<Vector2Int> treesToDestroy = new List<Vector2Int>();
        List<Vector2Int> treesToCreate = new List<Vector2Int>();
        for (int i = 0; i < 2 * numberSteps; i++)
        {
            for (int j = 0; j < 2 * numberSteps; j++)
            {
                bool isSurrounded = IsPositionSurroundedByTrees(new Vector2Int(i, j));
                Vector2Int position = new Vector2Int(i, j);
                if(HasTreeAtPosition(position) && !isSurrounded)
                {
                    treesToDestroy.Add(position);
                } else if (!HasTreeAtPosition(position) && isSurrounded)
                {
                    treesToCreate.Add(position);
                }
            }
        }

        DestroyTrees(treesToDestroy);
        CreateTrees(treesToCreate);
    }

    private bool IsPositionSurroundedByTrees(Vector2Int position)
    {
        return GetTreeNeighbours(position).Count() >= 4;
    }

    private IEnumerable<GameObject> GetTreeNeighbours(Vector2Int position)
    {
        return _createdTrees.Where(x =>
            {
                Vector2Int pos = x.Key;
                return Mathf.Abs(position.x - pos.x) <= 1 && Mathf.Abs(position.y - pos.y) <= 1;
            })
       .Select(x => x.Value);
    }

    private bool HasTreeAtPosition(Vector2Int position)
    {
        return _createdTrees.ContainsKey(position);
    }

    private void DestroyTrees(IEnumerable<Vector2Int> treeList)
    {
        foreach(Vector2Int pos in treeList)
        {
            DestroyTree(pos);
        }
    }

    private void DestroyTree(Vector2Int position)
    {
        GameObject tree;
        _createdTrees.TryGetValue(position, out tree);
        if(tree != null)
        {
            _createdTrees.Remove(position);
            Destroy(tree.gameObject);
        }
    }

    private void CreateTrees(IEnumerable<Vector2Int> treeList)
    {
        foreach(Vector2Int pos in treeList)
        {
            CreateTree(pos);
        }
    }

    private void CreateTree(Vector2Int position)
    {
        float noiseX = Alea.GetFloat(0.0f, noise);
        float noiseZ = Alea.GetFloat(0.0f, noise);
        Vector3 pos = new Vector3((position.x + noiseX - numberSteps) * stepSize, 0.0f, (position.y + noiseZ - numberSteps) * stepSize);
        GameObject tree = GenerateTree(pos);
        _createdTrees.Add(new Vector2Int(position.x, position.y), tree);
    }
}
