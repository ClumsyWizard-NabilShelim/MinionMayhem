﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
	private const int DIAGONAL_DISTANCE = 14;
	private const int HORIZONTAL_DISTANCE = 10;

	private PathRequestManager pathRequestManager;

	private void Awake()
	{
		pathRequestManager = GetComponent<PathRequestManager>();
	}

	public void StartFindPath(Vector2 startPosition, Vector2 endPosition)
	{
		StartCoroutine(FindPath(startPosition, endPosition));
	}

	private IEnumerator FindPath(Vector2 startPos, Vector2 endPos)
	{
		Node startNode = GridManager.Instance.NodeFromWorldPosition(startPos);
		Node endNode = GridManager.Instance.NodeFromWorldPosition(endPos);

		Vector2[] wayPoints = new Vector2[0];
		bool success = false;

		if(!endNode.Walkable)
		{
			foreach (Node neighbour in GridManager.Instance.GetNeighbours(endNode))
			{
				if (neighbour.Walkable)
				{
					endNode = neighbour;
					break;
				}
			}
		}

		if(endNode.Walkable)
		{
			Heap<Node> openList = new Heap<Node>(GridManager.Instance.GridMaxSize);
			HashSet<Node> closedList = new HashSet<Node>();

			openList.Add(startNode);

			while (openList.Count > 0)
			{
				Node currentNode = openList.RemoveFirst();
				closedList.Add(currentNode);

				if (currentNode == endNode)
				{
					success = true;
					break;
				}

				foreach (Node neighbour in GridManager.Instance.GetNeighbours(currentNode))
				{
					if (!neighbour.Walkable || closedList.Contains(neighbour))
						continue;

					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if(newMovementCostToNeighbour < neighbour.gCost || !openList.Contains(neighbour))
					{
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, endNode);
						neighbour.parent = currentNode;

						if (!openList.Contains(neighbour))
							openList.Add(neighbour);
						else
							openList.UpdateItem(neighbour);
					}
				}
			}
		}
		else
		{
			Debug.LogError("End node is unwalkable");
		}

		yield return null;

		if (success)
			wayPoints = RetracePath(startNode, endNode);

		pathRequestManager.FinishProcessingPath(wayPoints, success);
	}

	private Vector2[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		Vector2[] wayPoints = GetPathPoint(path);
		Array.Reverse(wayPoints);
		return wayPoints;
	}

	private Vector2[] GetPathPoint(List<Node> path)
	{
		List<Vector2> wayPoints = new List<Vector2>();

		for (int i = 0; i < path.Count; i++)
		{
			wayPoints.Add(path[i].WorldPosition);
		}

		return wayPoints.ToArray();
	}

	private int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
		int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

		if (dstX > dstY)
			return (14 * dstY + 10 * (dstX - dstY));
		else
			return (14 * dstX + 10 * (dstY - dstX));
	}
}