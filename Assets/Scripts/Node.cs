using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool unblocked;
    public Vector2 pos;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(bool aUnblocked, Vector3 aPos, int aGridX, int aGridY)
	{
		unblocked = aUnblocked;
		pos = aPos;
		gridX = aGridX;
		gridY = aGridY;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
