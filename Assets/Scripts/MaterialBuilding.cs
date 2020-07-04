using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBuilding : MonoBehaviour
{

    public float timeOnMaterial = 0.1f;
    public Material MaterialOff;
    public Material MaterialOn;

    float Timer;
    bool TurnOff = false;


    public void TurnOn()
    {
        GetComponent<MeshRenderer>().material = MaterialOn;
        Timer = Time.time + timeOnMaterial;
        TurnOff = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (TurnOff)
        {
            if (Timer < Time.time)
            {
                GetComponent<MeshRenderer>().material = MaterialOff;
                TurnOff = false;
            }
        }
    }
}
