using UnityEngine;

public class Line : MonoBehaviour
{
    [HideInInspector] public bool filled;

    [SerializeField] public LineRenderer line;
    [SerializeField] public Gradient startColor;
    [SerializeField] public Gradient endColor;

    public void Init(Vector3 start, Vector3 end)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.colorGradient = startColor;
        filled = false;
        Debug.Log("Line_Init");
    }

    public void Add()
    {
        filled = true;
        line.colorGradient = endColor;
        line.sortingOrder++;
        Debug.Log("Line_Add");

    }
}
