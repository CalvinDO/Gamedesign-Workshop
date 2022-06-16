using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWPoolManager : MonoBehaviour
{
    public Transform activeSpellPool;
    public Transform projectilePool;

    public static GWPoolManager instance;

    void Awake() {
        GWPoolManager.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
