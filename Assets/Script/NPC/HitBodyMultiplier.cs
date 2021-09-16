using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBodyMultiplier : MonoBehaviour
{
    public float multiplier;

    public GameObject mainObject;

    public void ApplyDamage(float dmg)
    {
        mainObject.SendMessage("ApplyDamage", multiplier * dmg, SendMessageOptions.DontRequireReceiver);
    }

}
