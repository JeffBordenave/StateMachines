using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
    }
}
