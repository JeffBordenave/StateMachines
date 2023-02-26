using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HarvestablePlot : Interactable
{
    public bool isSeedable = true;
    public bool isHarvestable = false;
    public Transform seedPos;

    private GameObject activeSeed;
    private GameObject activePlant;
    public float timeToGrow = 10;

    public void PlantSeed()
    {
        activeSeed = Instantiate(PlantManager.instance.seedPrefab, seedPos.position, Quaternion.identity);
    }

    public void Harvest()
    {
        print("je suis fort harvesté");
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (isHarvestable)
        {
            Harvest();
            isHarvestable = false;
            isSeedable = true;
            Destroy(activePlant);
        }
        else if(isSeedable)
        {
            PlantSeed();
            isSeedable = false;
        }
    }

    public void OnWater()
    {
        print("je suis totu mouillé");
        Destroy(activeSeed);
        StartCoroutine(GrowPlant(timeToGrow));
    }
    
    private IEnumerator GrowPlant(float waitTime)
    {
        Quaternion randomRotation = Quaternion.Euler(-90, 0, Random.Range(0, 90));
        activePlant = Instantiate(PlantManager.instance.plants[0], seedPos.position, randomRotation);

        for (int i = 1; i < PlantManager.instance.plants.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(activePlant);
            activePlant = Instantiate(PlantManager.instance.plants[i], seedPos.position, randomRotation);
        }

        isHarvestable = true;
        yield return null;
    }
}