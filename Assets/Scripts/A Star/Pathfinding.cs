using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public GameObject gameManager;
    public Transform startPosition;
    public Transform targetPosition;

    private void Awake()
    {
        grid =gameManager.GetComponent<Grid>();
    }
    private void Update()
    {
        FindPath(startPosition.position, targetPosition.position);
    }

    private void FindPath(Vector3 a_startPosition, Vector3 a_targetPosition)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_startPosition);
        Node TargetNode = grid.NodeFromWorldPosition(a_targetPosition);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node currentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].FCost<currentNode.FCost || OpenList[i].FCost == currentNode.FCost && OpenList[i].hCost<currentNode.hCost)
                {
                    currentNode = OpenList[i];
                }
            }
            OpenList.Remove(currentNode);
            ClosedList.Add(currentNode);

            if (currentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
                break;
            }

            foreach(Node neighborNode in grid.GetNeighboringNodes(currentNode))
            {
                if(!neighborNode.isWall || ClosedList.Contains(neighborNode))
                {
                    continue;
                }

                int MoveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);

                if (!OpenList.Contains(neighborNode) || MoveCost < neighborNode.FCost)
                {
                    neighborNode.gCost = MoveCost;
                    neighborNode.hCost = GetManhattenDistance(neighborNode, TargetNode);
                    neighborNode.parent = currentNode;

                    if (!OpenList.Contains(neighborNode))
                    {
                        OpenList.Add(neighborNode);
                    }
                }
            }
        }
    }

    private void GetFinalPath(Node a_startingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node currentNode = a_EndNode;

        while (currentNode!=a_startingNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        FinalPath.Reverse();
        grid.finalPath = FinalPath;
    }

    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
