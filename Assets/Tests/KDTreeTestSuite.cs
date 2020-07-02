using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Linq;

namespace Tests
{
    public class KDTreeTestSuite
    {
        [Test]
        public void TestNextDimension2D()
        {
            KDDimension dim = KDDimension.X;

            dim = KDTree.NextDimension(dim, 2);
            Assert.AreEqual(dim, KDDimension.Z);

            dim = KDTree.NextDimension(dim, 2);
            Assert.AreEqual(dim, KDDimension.X);
        }

        [Test]
        public void TestNextDimension3D()
        {
            KDDimension dim = KDDimension.X;

            dim = KDTree.NextDimension(dim, 3);
            Assert.AreEqual(dim, KDDimension.Y);

            dim = KDTree.NextDimension(dim, 3);
            Assert.AreEqual(dim, KDDimension.Z);

            dim = KDTree.NextDimension(dim, 3);
            Assert.AreEqual(dim, KDDimension.X);
        }

        [Test]
        public void TestTreeWithOneValue()
        {
            KDTree tree = new KDTree();
            tree.Add(GetGameObject());
            DumpTree(tree);
        }

        [Test]
        public void TestTreeWithThreeValues()
        {
            List<Vector3> positions = new List<Vector3>()
            {
                new Vector3(5, 1, 3),
                new Vector3(4, 4, 4),
                new Vector3(6, 6, 6)
            };

            KDTree tree = new KDTree();
            tree.Add(GetGameObjectsAtPos(positions));
            DumpTree(tree);
        }

        [Test]
        public void Test2DTreeWithSampleData1()
        {
            List<Vector2> positions = new List<Vector2>()
            {
                new Vector2(30, 40),
                new Vector2(5, 25),
                new Vector2(10, 12),
                new Vector2(70, 70),
                new Vector2(50, 30),
                new Vector2(35, 45)
            };
            KDTree tree = new KDTree(2);
            tree.Add(GetGameObjectsAt2DPos(positions));
            DumpTree(tree);
        }

        [Test]
        public void Test2DTreeWithSampleData2()
        {
            List<Vector2> positions = new List<Vector2>()
            {
                new Vector2(51, 75),
                new Vector2(70, 70),
                new Vector2(25, 40),
                new Vector2(35, 90),
                new Vector2(10, 30),
                new Vector2(55, 1),
                new Vector2(60, 80),
                new Vector2(50, 50),
                new Vector2(1, 10)
            };
            KDTree tree = new KDTree(2);
            tree.Add(GetGameObjectsAt2DPos(positions));
            DumpTree(tree);
        }

        private GameObject GetGameObject()
        {
            GameObject obj = new GameObject("Empty GO");
            return obj;
        }

        private GameObject GetGameObjectAtPos(Vector3 pos)
        {
            GameObject obj = GetGameObject();
            obj.transform.position = pos;
            return obj;
        }

        private GameObject GetGameObjectAt2DPos(Vector2 pos)
        {
            GameObject obj = GetGameObject();
            obj.transform.position = new Vector3(pos.x, 0.0f, pos.y);
            return obj;
        }

        private IEnumerable<GameObject> GetGameObjectsAtPos(IEnumerable<Vector3> pos)
        {
            return pos.Select(x => GetGameObjectAtPos(x));
        }

        private IEnumerable<GameObject> GetGameObjectsAt2DPos(IEnumerable<Vector2> pos)
        {
            return pos.Select(x => GetGameObjectAt2DPos(x));
        }

        private void DumpTree(KDTree tree)
        {
            Debug.Log("Tree");
            DumpNode(tree.GetFirstNode(), "", false);
        }

        private void DumpNode(KDNode node, string prefix, bool isLeft)
        {
            if (node != null && node.obj != null)
            {
                Debug.Log(prefix + (isLeft ? "|-- " : "\\-- ") + node.obj.transform.position.ToString() + " - " + node.splitDimension.ToString());
                string addedPrefix = isLeft ? "|   " : "    ";
                DumpNode(node.leftChild, prefix + addedPrefix, true);
                DumpNode(node.rightChild, prefix + addedPrefix, false);
            }
        }
    }
}
