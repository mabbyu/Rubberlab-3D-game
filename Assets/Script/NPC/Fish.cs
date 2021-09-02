using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Transform curTarget;
    public List<Transform> targets = new List<Transform>();
    public float maxWPDist;

    float curSpeed;
    public float swimSpeed;
    public float maxSpeed;

    public float rotateSpeed;

    public float minDist;

    public float playerDist;
    Transform player;


    // Start is called before the first frame update
    void Start()
    {
        curSpeed = swimSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        GameObject[] tar = GameObject.FindGameObjectsWithTag("WaypointFish");
        foreach(GameObject wp in tar)
        {
            var dist = Vector3.Distance(transform.position, wp.transform.position);
            if(dist <= maxWPDist)
            {
                targets.Add(wp.transform);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(curTarget)
        {
            var dist = Vector3.Distance(transform.position, curTarget.position);

            if(dist <= minDist)
            {
                NewWaypoint();
            }

            transform.position += transform.forward * curSpeed * Time.deltaTime;

            Quaternion targetRot = Quaternion.LookRotation(curTarget.transform.position - transform.position);
            //targetRot.x = 0;
            //targetRot.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);


            //transform.position = Vector3.MoveTowards(transform.position, curTarget.position, curSpeed * Time.deltaTime);
        }
        else
        {
            NewWaypoint();
        }

        if(player)
        {
            var pdist = Vector3.Distance(transform.position, player.position);
            if(pdist <= playerDist)
            {
                curSpeed = maxSpeed;
            }
            else
            {
                curSpeed = swimSpeed;
            }
        }
    }

    void NewWaypoint()
    {
        int index = Random.Range(0, targets.ToArray().Length);
        curTarget = targets[index];
    }
}
