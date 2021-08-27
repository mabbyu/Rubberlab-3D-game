using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject Enemy;

    public float ammount;
    public float ammountMultiplier;

    public float maxFireRate;
    private float curFireRate;

    private GameObject Player;

    private GameObject selectedSpawn;
    public GameObject[] Spawns;

    private int index;

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
        curFireRate -= Time.deltaTime * 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            spawnObj();
        }
    }

    void spawnObj ()
    {
        if(curFireRate <= 0 && ammount > 0)
        {
            index = Random.Range(0, Spawns.Length);
            selectedSpawn = Spawns[index];

            Instantiate(Enemy, selectedSpawn.transform.position, selectedSpawn.transform.rotation);

            ammount -= 1;
            curFireRate = maxFireRate;
        }
        else
        {
            return;
        }
    }
}
