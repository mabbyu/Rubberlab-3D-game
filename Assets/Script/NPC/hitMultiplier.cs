using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitMultiplier : MonoBehaviour
{
    public GameObject mainBody;

    public float multiplier;

    void Start()
    {
        
    }

    void ApplyDamage(float dmg)
    {
        mainBody.SendMessage("ApplyDamage", dmg * multiplier, SendMessageOptions.DontRequireReceiver);
        //mainBody.GetComponent<HealthControl>().ApplyDamage(dmg * multiplier);
    }
}
