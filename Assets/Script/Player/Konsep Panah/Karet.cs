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

    private string enemyTag;

    private bool didHit;

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Fly(Vector3 force)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse);
        rigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (didHit) return;
        didHit = true;

        if (collider.CompareTag(enemyTag))
        {
            var health = collider.GetComponent<HealthControl>();
            health.ApplyDamage(damage);    
        }

        print(collider.gameObject.name);
        //rigidbody.velocity = Vector3.zero;
        //rigidbody.angularVelocity = Vector3.zero;
        //rigidbody.isKinematic = true;
        //transform.SetParent(collider.transform);
    }
}