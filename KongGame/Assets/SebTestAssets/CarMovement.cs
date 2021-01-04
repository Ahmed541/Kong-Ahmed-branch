using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // probably just manually add these, idk.
    // don't really want to do a tag-based array filling since we need the waypoints to be in a particular order
    [SerializeField]private int numberOfWaypoints_i = 4;
    public GameObject[] waypoints_go;

    private GameObject currentWaypoint_go;
    private int currentWaypointIndex_i;
    [SerializeField] private int firstWaypointIndex_i = 0;
    private float distanceToCurrentWaypoint_f;

    public float speed_f;

    // raycast collision variables
    public GameObject rayOrigin_go;
    [Range(1, 15)]public float rayLength_f = 3;
    bool rayColl_b;
    RaycastHit hit;



    // Start is called before the first frame update
    void Start()
    {
        // fill the array of waypoints
        // this is code is mega shit since it relies on the game object names being really specific
        waypoints_go = new GameObject[numberOfWaypoints_i];
        for(int i = 0; i < numberOfWaypoints_i; i++)
        {
            string waypointName_s = "WayPoint" + i;
            waypoints_go[i] = GameObject.Find(waypointName_s);
        }

        currentWaypointIndex_i = firstWaypointIndex_i;
        currentWaypoint_go = waypoints_go[currentWaypointIndex_i];

        ChangeWaypoint(currentWaypointIndex_i);
    }

    // Update is called once per frame
    void Update()
    {
        rayColl_b = Physics.Raycast(rayOrigin_go.transform.position, rayOrigin_go.transform.forward, out hit, rayLength_f);
        if(!rayColl_b)
        {      
            Movement(0.1f); 
            currentWaypoint_go = waypoints_go[currentWaypointIndex_i];
            distanceToCurrentWaypoint_f = GetDistanceToWaypoint(currentWaypoint_go);


            Debug.Log("Currently Going To " + currentWaypoint_go);
            Debug.Log("Distance to Current Waypoint: " + distanceToCurrentWaypoint_f);

            // these conditions need to be changed so they aren't ==
            // cuz positions are very rarely exactly equal.
            // could force positions to be 2dp ?

            if(distanceToCurrentWaypoint_f <= 0.2) // it has reached its new waypoint
            {
                Debug.Log("You made it");
                currentWaypointIndex_i++;

                if (currentWaypointIndex_i > numberOfWaypoints_i - 1) // reset back to the first waypoint after completing a cycle
                    currentWaypointIndex_i = 0;

                ChangeWaypoint(currentWaypointIndex_i);
            }
        }
    }

    void Movement(float _speedFactor)
    {
        // just for now it'll constantly move forward
        transform.Translate(Vector3.forward * speed_f * _speedFactor);
    }

    void ChangeWaypoint(int _newWaypointIndex)
    {
        GameObject newWaypoint = waypoints_go[_newWaypointIndex];

        Debug.Log("Now Going To " + newWaypoint);

        // face the new waypoint
        transform.LookAt(newWaypoint.transform);
        // return x and z rotations to 0
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f); // dont want it moving at a random downwards angle y'know
    }

    // Find a distance value from the car to the next waypoint
    // so that it can change direction when it reaches said waypoint
    float GetDistanceToWaypoint(GameObject currentWP)
    {
        Vector3 differenceV3 = new Vector3(currentWP.transform.position.x - transform.position.x, currentWP.transform.position.y - transform.position.y, currentWP.transform.position.z - transform.position.z);
        float distToWP_f = Mathf.Sqrt(Mathf.Pow(differenceV3.x, 2f) + Mathf.Pow(differenceV3.y, 2f) + Mathf.Pow(differenceV3.z, 2f));
        return distToWP_f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(rayOrigin_go.transform.position, rayOrigin_go.transform.forward * rayLength_f);
    }
}
