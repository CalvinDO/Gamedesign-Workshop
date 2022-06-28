using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GWUIDisarmDisplay : MonoBehaviour
{
    public Image image;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.root.GetComponent<GWEnemyController>().stats.isDisarmed) {
            this.image.gameObject.SetActive(true);
        }
        else {
            this.image.gameObject.SetActive(false);

        }
    }
}
