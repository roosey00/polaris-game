using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


// 현재 안쓰임
public class Node
{
    public Vector3 position;  // 노드의 좌표 (2D 게임일 경우 Vector2 사용 가능)
    public Node parent;  // 경로 추적을 위한 부모 노드
    public float gCost;  // 시작 노드에서 이 노드까지의 비용
    public float hCost;  // 휴리스틱 비용 (이 노드에서 목표 노드까지 예상 비용)
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
        // 노드를 저장할 openList와 closedList
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        // 시작 노드 및 목표 노드
        Node startNode = new Node(startPos);
        Node goalNode = new Node(goalPos);

        // 시작 노드를 openList에 추가
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // fCost가 가장 낮은 노드를 찾음
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            // 현재 노드를 openList에서 제거하고 closedList에 추가
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // 목표 노드에 도달한 경우 경로를 역추적하여 반환
            if (currentNode.position == goalPos)
            {
                return RetracePath(startNode, currentNode);
            }

            // 인접한 노드 탐색 (8방향 혹은 4방향)
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor))
                {
                    continue;  // 이미 처리한 노드는 무시
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

        // 경로를 찾지 못한 경우 빈 리스트 반환
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

        // 예시: 4방향 또는 8방향 인접 노드를 가져오는 방식
        Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPos = node.position + direction;
            Node neighborNode = new Node(neighborPos);

            // 여기에서 실제로 맵 상에 해당 좌표가 유효한지 (장애물이 없는지) 체크해야 함
            // 예시: 맵에서 벽인지 확인하는 조건을 추가

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
