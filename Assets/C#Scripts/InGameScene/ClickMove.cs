using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


// ���� �Ⱦ���
public class Node
{
    public Vector3 position;  // ����� ��ǥ (2D ������ ��� Vector2 ��� ����)
    public Node parent;  // ��� ������ ���� �θ� ���
    public float gCost;  // ���� ��忡�� �� �������� ���
    public float hCost;  // �޸���ƽ ��� (�� ��忡�� ��ǥ ������ ���� ���)
    public float fCost { get { return gCost + hCost; } }  // fCost = gCost + hCost

    public Node(Vector3 pos)
    {
        position = pos;
    }
}


public class ClickMove : Moveable
{

    private List<Node> FindPath(Vector3 startPos, Vector3 goalPos)
    {
        // ��带 ������ openList�� closedList
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        // ���� ��� �� ��ǥ ���
        Node startNode = new Node(startPos);
        Node goalNode = new Node(goalPos);

        // ���� ��带 openList�� �߰�
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // fCost�� ���� ���� ��带 ã��
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            // ���� ��带 openList���� �����ϰ� closedList�� �߰�
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // ��ǥ ��忡 ������ ��� ��θ� �������Ͽ� ��ȯ
            if (currentNode.position == goalPos)
            {
                return RetracePath(startNode, currentNode);
            }

            // ������ ��� Ž�� (8���� Ȥ�� 4����)
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor))
                {
                    continue;  // �̹� ó���� ���� ����
                }

                float newGCost = currentNode.gCost + Vector3.Distance(currentNode.position, neighbor.position);
                if (newGCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    //neighbor.hCost = getDistance(neighbor.position, goalPos);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        // ��θ� ã�� ���� ��� �� ����Ʈ ��ȯ
        return new List<Node>();
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // ����: 4���� �Ǵ� 8���� ���� ��带 �������� ���
        Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPos = node.position + direction;
            Node neighborNode = new Node(neighborPos);

            // ���⿡�� ������ �� �� �ش� ��ǥ�� ��ȿ���� (��ֹ��� ������) üũ�ؾ� ��
            // ����: �ʿ��� ������ Ȯ���ϴ� ������ �߰�

            neighbors.Add(neighborNode);
        }

        return neighbors;
    }

    new private void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        targetPos = transform.position;
    }
}
