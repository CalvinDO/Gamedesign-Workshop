using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWMainCamera : MonoBehaviour
{
    public static GWMainCamera instance;


    void Awake() {
        GWMainCamera.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
