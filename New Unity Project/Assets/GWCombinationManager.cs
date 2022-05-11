using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCombinationManager : MonoBehaviour {

    public static GWCombinationManager instance;



    public GWElementCombinationTable combinationTable;

    void Awake() {
        GWCombinationManager.instance = this;
    }

    public static GWEType GetCombination(GWEType first, GWEType second) {

        int index = 0;

        Debug.Log(first + " " + second);

        foreach (GWEType firstElement in GWCombinationManager.instance.combinationTable.firstElements) {
            GWEType secondElement = GWCombinationManager.instance.combinationTable.secondElements[index];
            Debug.Log(firstElement + " " + secondElement);
            if (firstElement == first && secondElement == second) {
                return GWCombinationManager.instance.combinationTable.results[index];
            }

            if (firstElement == second && secondElement == first) {
                return GWCombinationManager.instance.combinationTable.results[index];
            }

            index++;
        }

        throw new System.Exception("no element combination!");
    }
}
