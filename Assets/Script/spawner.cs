using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject Enemy;

    GameObject currEnemy;

    private GameObject Player;
    
    private GameObject selectedSpawn;
    public GameObject[] Spawns;

    private int index;

    void Start()
    {
        
    }

    void Update()
    {
        if(!currEnemy)
        {
            spawnObj();
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    void spawnObj ()
    {
        index = Random.Range(0, Spawns.Length);
        selectedSpawn = Spawns[index];

        var a = Instantiate(Enemy, selectedSpawn.transform.position, selectedSpawn.transform.rotation);
        currEnemy = a;
    }
}
