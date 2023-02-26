using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    public GameObject[] harvestablePlot;
    public GameObject[] plants;
    public GameObject seedTrayPrefab;
    public GameObject seedPrefab;
    public GameObject waterPicker;
    public GameObject harvestDropObject;

    private void Awake()
    {
        instance = this;
    }

    public GameObject CheckForSeedablePlot()
    {
        for (int i = 0; i < harvestablePlot.Length; i++)
        {
            if (harvestablePlot[i].GetComponent<HarvestablePlot>().isSeedable)
            {
                return harvestablePlot[i];
            }
        }
        return null;
    }
    public GameObject CheckForHarvestablePlot()
    {
        for (int i = 0; i < harvestablePlot.Length; i++)
        {
            if (harvestablePlot[i].GetComponent<HarvestablePlot>().isHarvestable)
            {
                return harvestablePlot[i];
            }
        }
        return null;
    }
}
