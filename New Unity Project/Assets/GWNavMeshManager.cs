using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GWNavMeshManager : MonoBehaviour
{
    public static GWNavMeshManager instance;
    public NavMeshSurface navMeshSurface;
    void Awake() {
        GWNavMeshManager.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
