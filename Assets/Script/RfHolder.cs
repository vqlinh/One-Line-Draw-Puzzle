using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RfHolder : Singleton<RfHolder>
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private Point pointPrefab;
    [SerializeField] private List<Level> levels;
    private Dictionary<Vector2Int, Line> lines;
    private Dictionary<int, Point> points;
    private GameObject panelLevel;
    public TextMeshProUGUI txtNumberLv;
    public GameObject panel;
    private void Start()
    {
        panelLevel = GameObject.Find("PanelLevel");
        panelLevel.SetActive(false);
        lines = new Dictionary<Vector2Int, Line>();
        points = new Dictionary<int, Point>();
        Level levelStart = levels[0];
        LevelStart(levelStart);

    }
    private void Update()
    {
        txtNumberLv.text = LevelButton.Instance.nextLevel.ToString();
    }
    private void LevelStart(Level level)
    {
        for (int i = 0; i < level.Points.Count; i++)
        {
            Vector4 posData = level.Points[i];
            Vector3 spawnPos = new Vector3(posData.x, posData.y, posData.z);
            int id = (int)posData.w;
            points[id] = Instantiate(pointPrefab);
            points[id].Init(spawnPos, id);
        }

        for (int i = 0; i < level.Lines.Count; i++)
        {
            Vector2Int normal = level.Lines[i];
            Vector2Int reversed = new Vector2Int(normal.y, normal.x);
            Line spawnLine = Instantiate(linePrefab);
            lines[normal] = spawnLine;
            lines[reversed] = spawnLine;
            spawnLine.Init(points[normal.x].Position, points[normal.y].Position);
        }

        //lv.text = currentLevel.ToString();
    }
}
