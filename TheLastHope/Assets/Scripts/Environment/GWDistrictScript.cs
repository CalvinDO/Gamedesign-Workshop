using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDistrictScript : MonoBehaviour
{
    [SerializeField] private GWCityScript city;
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private GWSpawner spawner;
    [SerializeField] private GWEType element;
    [SerializeField] private int corruption; // -1 equals cleared

    public GameObject livingLeader;
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
    void Update()
    {
        if(!livingLeader && !spawner && barriers[0])
        {
            openBarriers();
        }
    } 

    public void openBarriers() // called on boss kill
    {
        foreach(GameObject barrier in barriers)
        {
            Destroy(barrier);
        }
        this.corruption = -1;
        city.increaseCorruption();
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
        spawner.spawning();
    }

    public void corrupt()
    {
        corruption++;
    }

    public int getCorruption()
    {
        return corruption;
    }
    public GWEType getElement()
    {
        return element;
    }
}
