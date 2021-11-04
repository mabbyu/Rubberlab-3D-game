using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public float maxEnemy;

    public GameObject Enemy;

    public bool isGuru;

    //GameObject[] currEnemy;

    private GameObject Player;
    
    private GameObject selectedSpawn;
    public GameObject[] Spawns;

    private int index;

    public List<GameObject> currEnemy = new List<GameObject>();

    int f = 1;

    void Start()
    {
        
    }

    void Update()
    {
        if(!isGuru)
        {
            if (currEnemy.ToArray().Length < maxEnemy)
            {
                spawnObj();
            }
        }
        else
        {
            if(f > 0)
            spawnObj();
        }
    }

   

    private void OnTriggerStay(Collider other)
    {

    }

    void spawnObj ()
    {
        f = 0;
        index = Random.Range(0, Spawns.Length);
        selectedSpawn = Spawns[index];

        var a = Instantiate(Enemy, selectedSpawn.transform.position, selectedSpawn.transform.rotation);
        if (!isGuru)
        a.GetComponent<HealthControl>().s = this;
        currEnemy.Add(a);
    }
}
