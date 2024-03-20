using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private Edge edgePrefab;
    [SerializeField] private Point pointPrefab;
    [SerializeField] private LineRenderer LineDraw;

    private Dictionary<int, Point> points;
    private Dictionary<Vector2Int, Edge> edges;
    private Point startPoint, endPoint;
    private int currentId;
    private bool isFinished;

    private void Awake()
    {
        isFinished = false;
        points = new Dictionary<int, Point>();
        edges = new Dictionary<Vector2Int, Edge>();
        LineDraw.gameObject.SetActive(false);
        currentId = -1;
        LevelStart();
    }

    private void LevelStart()
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = level.Col * 0.5f;
        camPos.y = level.Row * 0.5f;
        Camera.main.transform.position = camPos;
        Camera.main.orthographicSize = Mathf.Max(level.Col, level.Row) + 2f;

        for (int i = 0; i < level.Points.Count; i++)
        {
            Vector4 posData = level.Points[i];
            Vector3 spawnPos = new Vector3(posData.x, posData.y, posData.z);
            int id = (int)posData.w;
            points[id] = Instantiate(pointPrefab);
            points[id].Init(spawnPos, id);
        }

        for (int i = 0; i < level.Edges.Count; i++)
        {
            Vector2Int normal = level.Edges[i];
            Vector2Int reversed = new Vector2Int(normal.y, normal.x);
            Edge spawnEdge = Instantiate(edgePrefab);
            edges[normal] = spawnEdge;
            edges[reversed] = spawnEdge;
            spawnEdge.Init(points[normal.x].Position, points[normal.y].Position);
        }
    }

    private void Update()
    {
        if (isFinished) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (!hit) return;
            startPoint = hit.collider.gameObject.GetComponent<Point>();
            LineDraw.gameObject.SetActive(true);
            LineDraw.positionCount = 2;
            LineDraw.SetPosition(0, startPoint.Position);
            LineDraw.SetPosition(1, startPoint.Position);
        }
        else if (Input.GetMouseButton(0) && startPoint != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                endPoint = hit.collider.gameObject.GetComponent<Point>();
            }
            LineDraw.SetPosition(1, mousePos2D);
            if (startPoint == endPoint || endPoint == null) return;
            if (IsStartAdd())
            {
                currentId = endPoint.Id;
                edges[new Vector2Int(startPoint.Id, endPoint.Id)].Add();
                startPoint = endPoint;
                LineDraw.SetPosition(0, startPoint.Position);
                LineDraw.SetPosition(1, startPoint.Position);
            }
            else if (IsEndAdd())
            {
                currentId = endPoint.Id;
                edges[new Vector2Int(startPoint.Id, endPoint.Id)].Add();
                CheckWin();
                startPoint = endPoint;
                LineDraw.SetPosition(0, startPoint.Position);
                LineDraw.SetPosition(1, startPoint.Position);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LineDraw.gameObject.SetActive(false);
            startPoint = null;
            endPoint = null;
            CheckWin();
        }
    }

    private bool IsStartAdd()
    {
        if (currentId != -1) return false;
        Vector2Int edge = new Vector2Int(startPoint.Id, endPoint.Id);
        if (!edges.ContainsKey(edge)) return false;
        return true;
    }

    private bool IsEndAdd()
    {
        if (currentId != startPoint.Id)
        {
            return false;
        }

        Vector2Int edge = new Vector2Int(endPoint.Id, startPoint.Id);
        if (edges.TryGetValue(edge, out Edge result))
        {
            if (result == null || result.filled) return false;
        }
        else
        {
            return false;
        }
        return true;
    }

    private void CheckWin()
    {
        foreach (var item in edges)
        {
            if (!item.Value.filled)
            {
                return;
            }
        }
        isFinished = true;
        StartCoroutine(GameFinished());
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        print("Win Game");
    }
}