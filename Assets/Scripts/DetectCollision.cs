using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

public class DetectCollision : MonoBehaviour
{


    public float collisionRadius = 1.1f;
    public GameObject explotion;
    private GameObject _hand_1;
    private GameObject _hand_2;

    private GameObject building;

    void Start()
    {
        _hand_1 = GameObject.FindGameObjectWithTag("Hand1");
        _hand_2 = GameObject.FindGameObjectWithTag("Hand2");
    }
    void Update()
    {


        //Vector3 playerPoint1 = _hand_1.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        Vector3 playerPoint1 = _hand_1.transform.position;
        float playerRadius1 = Vector3.Distance(_hand_1.transform.position, playerPoint1);

        //Vector3 playerPoint2 = _hand_2.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        Vector3 playerPoint2 = _hand_2.transform.position;
        float playerRadius2 = Vector3.Distance(_hand_2.transform.position, playerPoint2);
        if ((Vector3.Distance(transform.position, _hand_1.transform.position) <= collisionRadius + playerRadius1) || (Vector3.Distance(transform.position, _hand_2.transform.position) <= collisionRadius + playerRadius2))
        {
            // Create explotion and activate buildings!
            GameObject clone;
            clone = Instantiate(explotion, transform.position, transform.rotation);
            Destroy(clone, 4);
            gameObject.SetActive(false);
            GameObject[] buildings;
            buildings = GameObject.FindGameObjectsWithTag("Building");
            foreach (GameObject building in buildings)
            {
                building.GetComponent<MaterialBuilding>().TurnOn();
            }

        }


    }
}
