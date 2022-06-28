using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWLoot : MonoBehaviour
{
    [SerializeField] private GWCollectableSpell collectible;
    [SerializeField] private GWSpell[] spells;
    [SerializeField] private GameObject splitBox;
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
        switch(this.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GWDistrictScript>().getElement())
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

    public void destroy()
    {
        GameObject fracturedBox = Instantiate(splitBox, this.transform.position, Quaternion.identity);
        spawnSpell();
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

    private void spawnSpell()
    {
        if(Random.Range(0, 100) > 70)
        {
            int elem = elementChance[Random.Range(0, elementChance.Count)];
            collectible.spell = spells[elem];
            Debug.Log(collectible.spell);
            Instantiate(collectible, this.transform.position, Quaternion.identity);
        }
    }
}
