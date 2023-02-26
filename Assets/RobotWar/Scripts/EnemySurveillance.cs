using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySurveillance : EnemyBehavior
{
    public delegate void myDelegate();
    public static event myDelegate OnKill;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void CheckForTarget(GameObject target)
    {
        if (team != target.GetComponent<EnemySurveillance>().team)
        {
            target.transform.Find("Head").GetComponent<Renderer>().material = transform.Find("Head").GetComponent<Renderer>().material;
            target.GetComponent<EnemySurveillance>().team = team;

            if (OnKill != null)
            {
                OnKill();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag) CheckForTarget(other.gameObject);
    }
}