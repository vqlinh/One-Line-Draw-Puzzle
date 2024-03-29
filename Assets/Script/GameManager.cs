using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private Point pointPrefab;
    [SerializeField] private List<Level> levels;
    [SerializeField] private LineRenderer LineDraw;

    private Canvas canvas;
    public int levelChoose;
    private Level currentLevel;
    private List<GameObject> listWave;
    private GameObject previousWave;
    public GameObject waveFormPrefabs;
    public List<GameObject> lineDraws;

    private int currentId;
    private bool isFinished;
    private GameObject panelWin;
    private Point startPoint, endPoint;
    private Dictionary<int, Point> points;
    private Dictionary<Vector2Int, Line> lines;

    private void Awake()
    {
        lineDraws = new List<GameObject>();
        listWave = new List<GameObject>();
        canvas = GameObject.Find("CanvasWaveForm").GetComponent<Canvas>();
        isFinished = false;
        points = new Dictionary<int, Point>();
        lines = new Dictionary<Vector2Int, Line>();
        LineDraw.gameObject.SetActive(false);
        currentId = -1;

        Level levelStart = levels[levelChoose];
        LevelStart(levelStart);
        panelWin = GameObject.Find("CompleteLevel");
        panelWin.SetActive(false);
    }

    public void NextLevel()
    {
        levelChoose = levels.IndexOf(currentLevel);
        if (levelChoose == -1 || levelChoose == levels.Count - 1) return;
        int nextIndex = levelChoose + 1;
        Level NextLevel = levels[nextIndex];
        ClearPreviousLevel();
        ClearWaveForm();
        LevelStart(NextLevel);
    }

    public void Replay()
    {
        if (!isFinished)
        {
            ClearPreviousLevel();
            ClearWaveForm();
            LevelStart(currentLevel);
        }

    }

    public void Undo()
    {
        if (!isFinished && lineDraws.Count > 0)
        {
            GameObject objectToRemove = lineDraws[lineDraws.Count - 1]; // Lấy reference đến game object cuối cùng trong danh sách
            lineDraws.RemoveAt(lineDraws.Count - 1); // Xóa phần tử cuối cùng trong danh sách
            Destroy(objectToRemove); // Hủy game object cuối cùng
        }
    }
 

    private void ClearWaveForm()
    {
        foreach (var wave in listWave)
        {
            Destroy(wave);
        }
        listWave.Clear();
    }

    private void ClearPreviousLevel()
    {
        startPoint = null;
        endPoint = null;
        currentId = -1;
        isFinished = false;
        foreach (var point in points.Values)
        {
            Destroy(point.gameObject);
        }
        points.Clear();

        foreach (var line in lines.Values)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
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
        currentLevel = level;
    }

    private void Update()
    {
        if (isFinished) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero); //Thực hiện raycast từ vị trí chuột để xem có bắn trúng một collider nào không.
            if (!hit) return;
            startPoint = hit.collider.gameObject.GetComponent<Point>(); //Lấy đối tượng Point từ collider được trúng và gán cho startPoint.
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
            if (hit) endPoint = hit.collider.gameObject.GetComponent<Point>();
            LineDraw.SetPosition(1, mousePos2D);
            if (startPoint == endPoint || endPoint == null) return;
            if (IsConnectLine())
            {
                currentId = endPoint.Id;
                lines[new Vector2Int(startPoint.Id, endPoint.Id)].Add();
                startPoint = endPoint;
                LineDraw.SetPosition(0, startPoint.Position);
                LineDraw.SetPosition(1, startPoint.Position);
                WaveForm(startPoint.Position);
                lineDraws.Add(LineDraw.gameObject);
                Debug.Log("LineDraws : "  +lineDraws.Count);
            }
            else if (IsEndConnect())
            {
                currentId = endPoint.Id;
                lines[new Vector2Int(startPoint.Id, endPoint.Id)].Add();
                CheckToWin();
                startPoint = endPoint;
                LineDraw.SetPosition(0, startPoint.Position);
                LineDraw.SetPosition(1, startPoint.Position);
                WaveForm(startPoint.Position);
                lineDraws.Add(LineDraw.gameObject);
                Debug.Log("LineDraws : " + lineDraws.Count);

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LineDraw.gameObject.SetActive(false);
            startPoint = null;
            endPoint = null;
            CheckToWin();
        }
    }

    private bool IsConnectLine() // Kiểm tra xem có thể bắt đầu thêm cạnh mới từ điểm hiện tại không.
    {
        if (currentId != -1) return false;
        Vector2Int line = new Vector2Int(startPoint.Id, endPoint.Id);
        if (!lines.ContainsKey(line)) return false; // check xem lúc bắt đầu game có line giữa 2 điểm chưa, nếu k có thì false ( nếu true thì vẽ dược)
        return true;
    }

    private bool IsEndConnect() //Kiểm tra xem có thể kết thúc cạnh ở điểm hiện tại không.
    {
        if (currentId != startPoint.Id) return false;

        Vector2Int line = new Vector2Int(endPoint.Id, startPoint.Id);
        if (lines.TryGetValue(line, out Line result))
        {
            if (result == null || result.filled) return false;
        }
        else return false;

        return true;
    }
    private void WaveForm(Vector3 position)
    {
        if (waveFormPrefabs != null)
        {
            if (previousWave != null)
            {
                previousWave.SetActive(false);
            }
            GameObject waveForm = Instantiate(waveFormPrefabs, canvas.transform);
            waveForm.transform.position = position;
            previousWave = waveForm;
            listWave.Add(waveForm);
        }
        if (isFinished)
        {
            for (int i = 0; i < listWave.Count; i++)
            {
                GameObject gameObject = listWave[i];
                gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(2f);
        panelWin.SetActive(true);
    }

    private void CheckToWin()
    {
        foreach (var item in lines)
        {
            if (!item.Value.filled) return;
        }
        isFinished = true;
        StartCoroutine(GameFinished());
    }
}
