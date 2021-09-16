using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject Enemy;

    public float ammount;
    public float ammountMultiplier;

    private GameObject Player;

    private GameObject selectedSpawn;
    public GameObject[] Spawns;

    private int index;

    GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        if(ammountMultiplier != 0)
        {
            ammount = Random.Range((ammount - ammountMultiplier), (ammount + ammountMultiplier));
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnObj();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
        }
    }

    void spawnObj ()
    {
        if(ammount > 0)
        { 
            if(currentEnemy == null)
            {
                index = Random.Range(0, Spawns.Length);
                selectedSpawn = Spawns[index];

                var A = Instantiate(Enemy, selectedSpawn.transform.position, selectedSpawn.transform.rotation);
                currentEnemy = A;

                ammount -= 1;
            }
        }
        else
        {
            return;
        }
    }
}
