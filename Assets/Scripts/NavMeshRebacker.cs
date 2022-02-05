using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebacker : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        LabirinthSpawner.NavMeshRebakerEvent.AddListener(BuildNavMeshSurface);
    }
    private void BuildNavMeshSurface()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
