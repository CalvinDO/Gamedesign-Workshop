using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private GameObject districtLeader;
    [SerializeField] private GWDistrictScript district;
    [SerializeField] private List<GameObject> spawnPoints;

     // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    public void spawning()
    {
        foreach (GameObject point in spawnPoints)
        {
            if(point.tag == "DistrictLeader")
            {
                Vector3 pos = new Vector3(point.transform.position.x,0,point.transform.position.z);
                district.livingLeader = Instantiate(this.districtLeader, pos, Quaternion.identity);
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

    public void spawnEnemy(GameObject enemy, Vector3 origin)
    {
        Vector3 pos = new Vector3((origin.x + Random.Range(-5, 5)),0,(origin.z + Random.Range(-5, 5)));
        Instantiate(enemy, pos, Quaternion.identity);
    }
}
