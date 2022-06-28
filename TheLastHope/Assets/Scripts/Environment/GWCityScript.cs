
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCityScript : MonoBehaviour
{
    [SerializeField] private GWDistrictScript[] districts;
    // Start is called before the first frame update
     void Start()
    {
        districts[0].closeBarriers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseCorruption()
   {
        for(int i = 0; i < 5; i++)
        {
            GWDistrictScript chosenDistrict = districts[Random.Range(0, districts.Length)]; //random.next() https://stackoverflow.com/questions/14297853/how-to-get-random-values-from-array-in-c-sharp
            // call corrupt from chosenDistrict if chosenDistrict corruption >= 0
            if(chosenDistrict.getCorruption() != -1)
            {
                chosenDistrict.corrupt();
                i += 5;
            }
        }     
    }
}
