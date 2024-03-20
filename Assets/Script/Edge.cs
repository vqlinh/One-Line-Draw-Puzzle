using UnityEngine;

public class Edge : MonoBehaviour
{
    [HideInInspector] public bool filled;

    [SerializeField] private LineRenderer line;
    [SerializeField] private Gradient startColor;
    [SerializeField] private Gradient activeColor;

    public void Init(Vector3 start, Vector3 end)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.colorGradient = startColor;
        filled = false;
    }

    public void Add()
    {
        filled = true;
        line.colorGradient = activeColor;
    }
}
