using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWLoot : MonoBehaviour
{
    [SerializeField] private GWCollectableSpell collectible;
    [SerializeField] private GameObject splitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void destroy()
    {
        GameObject fracturedBox = Instantiate(splitBox, this.transform.position, Quaternion.identity);
        if(collectible)
        {
            Instantiate(collectible, this.transform.position, Quaternion.identity);
        }
        Rigidbody[] rbOfParts = fracturedBox.GetComponentsInChildren<Rigidbody>();
        if(rbOfParts.Length > 0)
        {
            foreach (var rb in rbOfParts)
            {
                rb.AddExplosionForce(500, transform.position, 1);
            }
        }
        Destroy(this.gameObject);
    }
}
