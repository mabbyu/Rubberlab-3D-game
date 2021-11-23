using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karet : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float torque;

    [SerializeField]
    private Rigidbody rigidbody;

    public string enemyTag;

    private bool didHit;

    public bool karetEnemy;

    float ct = 1;

    public bool canPick;

    public bool isFired;

    public void Fly(Vector3 force)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse);
        rigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    void OnTriggerStay(Collider collider)
    {
        if (didHit) return;
        didHit = true;

        if(collider.CompareTag(enemyTag) || collider.CompareTag("Player"))
        {
            if (karetEnemy)
            {
                collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                if (collider.CompareTag(enemyTag))
                {
                    //var health = collider.GetComponent<HealthControl>();
                    //health.ApplyDamage(damage);
                    collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                    print("hitEnemy");
                }
            }

            print(collider.gameObject.name);
        }

        

        
        //rigidbody.velocity = Vector3.zero;
        //rigidbody.angularVelocity = Vector3.zero;
        //rigidbody.isKinematic = true;
        //transform.SetParent(collider.transform);
    }

    void Update()
    {
        ct -= Time.deltaTime;
        if (ct <= 0)
        {
            canPick = true;
        }
    }
}