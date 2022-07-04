using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWLoot : MonoBehaviour {
    [SerializeField] private GWCollectableSpell collectible;
    [SerializeField] private GWSpell[] spells;
    [SerializeField] private GameObject splitBox;
    public List<int> elementChance = new List<int>();

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                elementChance.Add(i);
            }
        }
       
        int elem;
        switch (this.gameObject.GetComponentInParent<GWDistrictScript>().getElement()) {
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
        for (int i = 0; i < 84; i++) {
            elementChance.Add(elem);
        }
    }

    public void destroy() {
        GameObject fracturedBox = Instantiate(splitBox, this.transform.position, Quaternion.identity);
        spawnSpell();
        Rigidbody[] rbOfParts = fracturedBox.GetComponentsInChildren<Rigidbody>();
        if (rbOfParts.Length > 0) {
            foreach (var rb in rbOfParts) {
                rb.AddExplosionForce(500, transform.position, 1);
            }
        }
        fracturedBox.GetComponent<GWDestroySound>().PlayBoxDestroy(fracturedBox.GetComponent<AudioSource>());
        //Debug.Log("Lootbox destroy");
        Destroy(fracturedBox, 5);
        Destroy(this.gameObject);
    }

    private void spawnSpell() {
        if (Random.Range(0, 100) > 70) {
            int elem = elementChance[Random.Range(0, elementChance.Count)];
            switch(elem)
            {
                case 0: //GWEType.EARTH: //Order: Earth, fire, water, air public Vector4 sensibilities = new Vector4( 0.8f, 0.4f, 0.1f, 0.15f);
                    collectible.spell = spells[elem];
                    break;
                case 1: //GWEType.FIRE:
                    collectible.spell = spells[elem];
                    break;
                case 2: //GWEType.WATER:
                    if(Random.Range(0,1) > 0.5f)
                    {
                        collectible.spell = spells[elem];
                    }else{
                        collectible.spell = spells[4];
                    }
                    break;
                default: //AIR
                    collectible.spell = spells[elem];
                    break;
            }
            
            Debug.Log(collectible.spell);
            Instantiate(collectible, this.transform.position, Quaternion.identity);
        }
    }
}
