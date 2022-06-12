using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]


//diese Klasse macht elementcombinationen
public class GWElementCombinationTable : ScriptableObject {

    public List<GWEType> firstElements;
    public List<GWEType> secondElements;
    public List<GWEType> results;
}
