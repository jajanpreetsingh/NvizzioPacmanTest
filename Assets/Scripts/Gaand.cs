using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Gaand : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> Walls;

    public Material WallMat;

    public bool Refresh = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Refresh)
        {
            foreach (var item in Walls)
            {
                item.material = WallMat;
            }
            Refresh = false;
        }
    }
}