using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpawner : MonoBehaviour
{
    [SerializeField] private List<GWEnemyController> enemyList;
    [SerializeField] private GWEnemyController districtLeader;
    [SerializeField] private GWDistrictScript district;
    [SerializeField] private GWElementColorTable table;
    [SerializeField] private List<GameObject> spawnPoints;
    private List<int> elementChance = new List<int>();

     // Start is called before the first frame update


    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                elementChance.Add(i);
            }
        }
        int elem;
        switch(district.getElement())
        {
            case GWEType.EARTH:
                elem = 0;
                break;
            case GWEType.FIRE:
                elem = 1;
                break;
            case GWEType.WATER:
                elem = 2;
                break;
            default:
                elem = 3;
                break;
        }
        for(int i = 0; i < 84; i++)
        {
            elementChance.Add(elem);
        }
    }

    // Update is called once per frame

    public void spawning()
    {
        foreach (GameObject point in spawnPoints)
        {
            if(point.tag == "DistrictLeader")
            {
                Vector3 pos = new Vector3(point.transform.position.x,0,point.transform.position.z);
                district.livingLeader = Instantiate(this.districtLeader.gameObject, pos, Quaternion.identity);
            }else{
                int enemy = Random.Range(0, (enemyList.Count - 1));
                for(int i = 0; i < (2 * (district.getCorruption() + 1)); i++)
                {
                    spawnEnemy(this.enemyList[enemy], point.transform.position);
                }
            }
            
            Destroy(this.gameObject);
        }
    }

    public void spawnEnemy(GWEnemyController enemy, Vector3 origin)
    {
        Vector3 pos = new Vector3((origin.x + Random.Range(-5, 5)),0,(origin.z + Random.Range(-5, 5)));
        GWEnemyController newEnemy = Instantiate(enemy.gameObject, pos, Quaternion.identity).GetComponent<GWEnemyController>();
        chooseElement(newEnemy);
    }

    public void chooseElement(GWEnemyController enemy)
    {
        int elem = elementChance[Random.Range(0, elementChance.Count)];
        
        Renderer enemyRenderer = enemy.mesh.GetComponent<Renderer>();
        Color color = table.color[elem];
        enemyRenderer.material.color = color;
        
        switch(elem)
        {
            case 0: //GWEType.EARTH: //Order: Earth, fire, water, air public Vector4 sensibilities = new Vector4( 0.8f, 0.4f, 0.1f, 0.15f);
                enemy.stats.sensibilities = new Vector4( 0.1f, 0.8f, 0.5f, 1.0f);
                break;
            case 1: //GWEType.FIRE:
                enemy.stats.sensibilities = new Vector4( 0.8f, 0.1f, 1.0f, 0.5f);
                break;
            case 2: //GWEType.WATER:
                enemy.stats.sensibilities = new Vector4( 1.0f, 0.5f, 0.1f, 0.8f);
                break;
            default: //AIR
                enemy.stats.sensibilities = new Vector4( 0.5f, 1.0f, 0.8f, 0.1f);
                break;

        }
    }
}
