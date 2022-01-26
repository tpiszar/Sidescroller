using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
	public Vector2 gridSize;
	public float nodeSize;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;

	void Start()
	{
		nodeDiameter = nodeSize * 2;
		gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
		CreateGrid();
	}

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector2 worldBotLeft = new Vector2(transform.position.x, transform.position.y)
			- Vector2.right * gridSize.x / 2 - Vector2.up * gridSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector2 pos = worldBotLeft + Vector2.right * (x * nodeDiameter + nodeSize) + Vector2.up * (y * nodeDiameter + nodeSize);
				Collider2D selNode = Physics2D.OverlapCircle(pos, nodeSize);
				if (selNode != null && (selNode.gameObject.layer == 3 || selNode.gameObject.layer == 2))
                {
					selNode = null;
                }
				bool unblocked = (selNode == null);
				grid[x, y] = new Node(unblocked, pos, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node GetNode(Vector3 aPos)
	{
		float percentX = (aPos.x + gridSize.x / 2) / gridSize.x;
		float percentY = (aPos.y + gridSize.y / 2) / gridSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));

        if (grid != null)
        {

            foreach (Node n in grid)
            {

                Gizmos.color = (n.unblocked) ? Color.white : Color.red;
                Gizmos.DrawCube(n.pos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
