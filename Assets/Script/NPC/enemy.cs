using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public Transform player;

    public Transform spotter;

    public float minDis; //chase
    public float attackDis; //attack
    public float rotateSpeed;

    public float wayPointDist;
    
    Transform curWP;
    public List<Transform> wp = new List<Transform>();

    public float damage;
    public float moveSpeed;
    float currSpeed;

    public GameObject Bullet;
    public Transform spawn;

    public float fireRate;
    private float curFireRate;

    public AudioClip fireSound;

    public bool seePlayer;
    private RaycastHit hit;

    public float bulletSpeed;

    bool isAttacking;

    //footstep
    bool isWalking;
    
    public float FootstepDelayTime;

    float wt;

    public AudioSource audioFootstep;

    //angry
    bool isChasing;
    public AudioSource angryAudio;

    //animation
    Animator animator;

    public Animator boy;
    public Animator girl;

    void Start()
    {
        curFireRate = fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject[] tar = GameObject.FindGameObjectsWithTag("Way Point");
        foreach (GameObject wpa in tar)
        {
            wp.Add(wpa.transform);
        }

        currSpeed = moveSpeed;
        NewWaypoint();

        int index = Random.Range(0, 2);
        if(index == 1)
        {
            animator = boy;
            boy.gameObject.SetActive(true);
            girl.gameObject.SetActive(false);
        }
        else
        {
            animator = girl;
            girl.gameObject.SetActive(true);
            boy.gameObject.SetActive(false);
        }    
    }

    void Update()
    {
        if (isAttacking)
        {
            target = player;

            curFireRate -= Time.deltaTime * 1;

            spotter.transform.LookAt(target);

            var targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            var distance = Vector3.Distance(target.position, transform.position);

            transform.LookAt(targetPos);

            if (distance <= minDis && curFireRate <= 0) //ChasePlayer();
            {
                if (seePlayer)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * currSpeed);
                    isChasing = true;
                    angryAudio.Play();
                }
                else
                {
                    isChasing = false;
                    angryAudio.Stop();
                }
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * currSpeed);
                isAttacking = false;
            }

            if (distance <= attackDis && curFireRate <= 0)
            {
                if (seePlayer)
                    Fire();
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

            transform.Translate(Vector3.forward * Time.deltaTime * currSpeed);
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
        if (dist2 <= attackDis)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            if (target == player)
            {
                NewWaypoint();
            }
        }
        //footstep
        if (dist2 >= minDis)
        {
            isWalking = true;
            wt -= Time.deltaTime;
        }
        else
        {
            isWalking = false;
            audioFootstep.Stop();
        }

        if (wt <= 0)
        {
            wt = FootstepDelayTime;
            audioFootstep.Play();
        }

        animator.SetFloat("currSpeed", currSpeed);
        animator.SetBool("isAttacking", isAttacking);
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

    void Fire()
    {

        var shotBullet = Instantiate(Bullet, spawn.transform.position, spawn.transform.rotation);
        //Instantiate(MFlash, spawn.transform.position, spawn.transform.rotation);

        //shotBullet.GetComponent(bullet).damage = damage;
        //shotBullet.transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        var force = spawn.TransformDirection(Vector3.forward * bulletSpeed);
        shotBullet.GetComponent<Karet>().Fly(force);
        shotBullet.GetComponent<Karet>().karetEnemy = true;
        shotBullet.GetComponent<Karet>().isFired = true;

        GetComponent<AudioSource>().PlayOneShot(fireSound);
        curFireRate = fireRate;

    }
}
