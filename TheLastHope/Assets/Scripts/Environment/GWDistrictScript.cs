using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDistrictScript : MonoBehaviour
{
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private int corruption; // -1 equals cleared
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject barrier in barriers)
        {
            Renderer barrierRenderer = barrier.gameObject.GetComponent<Renderer>();
            Color color = Color.green;
            color.a = 0;
            barrierRenderer.material.color = color;
        }
    }

    // Update is called once per frame
/*     void Update()
    {
        
    } */

    public void openBarriers() // called on boss kill
    {
        foreach(GameObject barrier in barriers)
        {
            Renderer barrierRenderer = barrier.gameObject.GetComponent<Renderer>();
            Color color = Color.green;
            color.a = 0;
            barrierRenderer.material.color = color;
            BoxCollider col = barrier.GetComponent(typeof(BoxCollider)) as BoxCollider;
            Destroy(col);
        }
    }

    public void closeBarriers() // called on collision 
    {
        foreach(GameObject barrier in barriers)
        {
            if(corruption != -1)
            {
                Renderer barrierRenderer = barrier.gameObject.GetComponent<Renderer>();
                Color color = Color.red;
                color.a = 1;
                barrierRenderer.material.color = color;
                BoxCollider col = barrier.GetComponent(typeof(BoxCollider)) as BoxCollider;
                col.isTrigger = false;
            }
        }

        //Spawn Enemies
    }

    public void corrupt()
    {
        corruption++;
    }

    public int getCorruption()
    {
        return corruption;
    }
}
