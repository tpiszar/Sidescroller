using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
	public NodeGrid grid;

	public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.GetNode(startPos);
		Node targetNode = grid.GetNode(targetPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0)
		{
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
				{
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode)
			{
				List<Node> path = new List<Node>();
				Node currentNode = targetNode;

				while (currentNode != startNode)
				{
					path.Add(currentNode);
					currentNode = currentNode.parent;
				}
				path.Reverse();

				return path;
			}

			foreach (Node neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.unblocked || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		return null;
	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (distX > distY)
        {
			return 14 * distY + 10 * (distX - distY);
        }
		return 14 * distX + 10 * (distY - distX);
	}
}
