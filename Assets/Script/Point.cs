using UnityEngine;

public class Point : MonoBehaviour
{
    [HideInInspector] public int Id;
    [HideInInspector] public Vector3 Position;

    public void Init(Vector3 pos, int id)
    {
        Id = id;
        Position = pos; 
        transform.position = Position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
