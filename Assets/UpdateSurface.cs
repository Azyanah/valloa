using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateSurface : MonoBehaviour
{
    public NavMeshSurface surface;
    void Start()
    {
        surface.BuildNavMesh();        
    }

    // Update is called once per frame
    void Update()
    {
        surface.BuildNavMesh();   
    }
}
