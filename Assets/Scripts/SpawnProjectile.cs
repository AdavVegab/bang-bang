using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject AudioTrack;
    public GameObject projectile;
    public float FirstAfter = 56;
    public float DelayBetweenProjectiles = 0.3f;
    public bool StartWithLeft = false;
    public bool Double = false;
    public float DistanceFromCenter = 0.4f;
    public float DestroyAfter = 20;
    public float Correction = 8;
    
    public int nrOfProj = 3;

    bool next = true;
    int nrCreated = 0;
    float Timer;
    Vector3 projPosition;
    float offset;

    public int BeatCount = 0;

    // Start is called before the first frame update


    void Start()
    {
        Timer = Time.time + FirstAfter - Correction;
        AudioTrack.GetComponent<AudioSource>().Play(0);
        if (StartWithLeft)
        {
            next = false;
        }
    }

    void Update()
    {
        

        if (Timer < Time.time)
        { //This checks wether real time has caught up to the timer  

            if (Double)
            {
                projPosition = new Vector3(transform.position.x + DistanceFromCenter, transform.position.y, transform.position.z);
                GameObject clone;
                clone = Instantiate(projectile, projPosition, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
                Destroy(clone, DestroyAfter);
                projPosition = new Vector3(transform.position.x - DistanceFromCenter, transform.position.y, transform.position.z);
                GameObject clone1;
                clone1 = Instantiate(projectile, projPosition, transform.rotation);
                clone1.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
                Destroy(clone, DestroyAfter);

            } else
            {
                if (next)
                {
                    offset = DistanceFromCenter;
                    next = false;
                }
                else
                {
                    offset = -DistanceFromCenter;
                    next = true;
                }

                projPosition = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
                GameObject clone;
                clone = Instantiate(projectile, projPosition, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
                Destroy(clone, DestroyAfter);
            }

            Timer = Time.time + DelayBetweenProjectiles; //This sets the timer 0.3 seconds into the future
            nrCreated += 1;
            if (nrCreated == nrOfProj)
            {
                Timer = Time.time + 10000;
            }
        }
    }

   
}
