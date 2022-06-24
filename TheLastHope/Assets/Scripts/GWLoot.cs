using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWLoot : MonoBehaviour
{
    [SerializeField] private GWCollectableSpell collectible;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void breaking()
    {
        if(Random.Range(0, 100) >= 50)
        {
            Vector3 pos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            //add spell specifics here
            Instantiate(collectible, pos, Quaternion.identity);
        }
    }
}
