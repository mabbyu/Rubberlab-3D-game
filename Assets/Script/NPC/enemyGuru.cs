using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyGuru : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public Transform player;

    public Transform spotter;

    public float minDis; //chase
    public float rotateSpeed;

    public float wayPointDist;

    Transform curWP;
    public List<Transform> wp = new List<Transform>();

    public float damage;
    public float moveSpeed;

    public Transform spawn;

    public bool seePlayer;
    private RaycastHit hit;


    bool isAttacking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject[] tar = GameObject.FindGameObjectsWithTag("Way Point");
        foreach (GameObject wpa in tar)
        {
            wp.Add(wpa.transform);
        }

        NewWaypoint();
    }

    void Update()
    {
        if (isAttacking)
        {
            target = player;

            spotter.transform.LookAt(target);

            var targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            var distance = Vector3.Distance(target.position, transform.position);

            transform.LookAt(targetPos);

            if (distance <= minDis) //ChasePlayer();
            {
                if (seePlayer)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                }
                else
                {

                }
            }

            Vector3 dir = spotter.transform.TransformDirection(new Vector3(0, 0, 1));
            Vector3 pos = spotter.transform.position;

            if (Physics.Raycast(pos, dir, out hit, 10000))
            {
                if (hit.collider.tag == "Player")
                {
                    seePlayer = true;
                }
                else
                {
                    seePlayer = false;
                }
            }
        }
        else //patroli
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            spotter.transform.LookAt(target);

            var targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);

            transform.LookAt(targetPos);

            var dist = Vector3.Distance(transform.position, target.position);


            if (dist <= wayPointDist)
            {
                NewWaypoint();
            }
        }
        var dist2 = Vector3.Distance(transform.position, player.transform.position);
        if (dist2 <= minDis)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    /*void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }*/

    void NewWaypoint()
    {
        int index = Random.Range(0, wp.ToArray().Length);
        curWP = wp[index];
        target = curWP;
    }
}