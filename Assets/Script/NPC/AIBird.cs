using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBird : MonoBehaviour
{
    //target
    public Transform curTarget;
    Transform player;
    Transform curWP;
    public Transform[] wp;
    Vector3 lastPos;

    public bool attacking;

    public GameObject bombObject;
    int ammo = 1;

    //tower
    public Transform curTower;
    
    //movement
    float curSpeed;
    public float normalSpeed;

    float curRot;
    public float attackRot;
    public float normRot;

    //dist
    public float minDist; //dist to waypoint
    public float diveDist; //dist bfore dive
    public float playerDist; //dist from player
    public float towerDist; //dist from tower to player

    public float diveDepth;

    //Animation
    public GameObject animObject;
    public string flyAnim;

    //audio
    public AudioClip soundAttack;
    int aValue = 1;
    GameObject a;
    GameObject lp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        curSpeed = normalSpeed;
        curRot = normRot;
        NewWaypoint();

        a = new GameObject("Dive point");
        lp = new GameObject("Attack point");
    }


    // Update is called once per frame
    void Update()
    {
        animObject.GetComponent<Animation>().CrossFade(flyAnim);

        if(attacking)
        {
            curRot = attackRot;
            if (!curWP)
                NewWaypoint();


            Vector3 playerY = new Vector3(player.position.x, player.position.y + diveDepth, player.position.z);
            a.transform.position = playerY;

            if (curTarget == lp.transform) //diving
            {
                var distP = Vector3.Distance(transform.position, lp.transform.position);
                if (distP <= playerDist)
                {
                    NewWaypoint();

                    if(ammo > 0)
                    {
                        ammo = 0;
                        Instantiate(bombObject, transform.position, transform.rotation);
                    }
                }

                if(aValue > 0)
                {
                    aValue = 0;
                    GetComponent<AudioSource>().PlayOneShot(soundAttack);
                }

            }
            else if(curTarget == curWP) //patroll / after dive
            {
                

                var distW = Vector3.Distance(transform.position, curWP.position);
                if (distW <= minDist)
                {
                    curTarget = a.transform;
                }

                lastPos = player.transform.position;
                aValue = 1;
                ammo = 1;
            }
            else //moving to player
            {
                lp.transform.position = player.transform.position;

                var distD = Vector3.Distance(transform.position, curTarget.position);
                if (distD <= diveDist)
                {
                   
                    curTarget = lp.transform;
                }
            }
            
        }
        else
        {
            curRot = normRot;
            var dist = Vector3.Distance(transform.position, curTarget.position);

            if (dist <= minDist)
            {
                NewWaypoint();
            }
            
        }
        var dist2 = Vector3.Distance(transform.position, player.transform.position);
        if (dist2 <= towerDist)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }

    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * curSpeed * Time.deltaTime;

        Quaternion targetRot = Quaternion.LookRotation(curTarget.transform.position - transform.position);
        //targetRot.x = 0;
        //targetRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, curRot * Time.deltaTime);
    }

    void NewWaypoint()
    {
        int index = Random.Range(0, wp.Length);
        curWP = wp[index];
        curTarget = curWP;
    }

    private void OnDrawGizmos()
    {
        //bird to player
        if (attacking)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.yellow;

        if(curTarget)
        Gizmos.DrawLine(transform.position, curTarget.transform.position);

        if (attacking)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.blue;

        if (curTower && player)
        Gizmos.DrawLine(curTower.transform.position, player.transform.position);
    }
}
