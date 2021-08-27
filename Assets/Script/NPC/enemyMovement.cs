using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform transform_Player;
    //private float rotSpeed = 3f;
    private float moveSpeed = 6f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform_Player.position), rotSpeed * Time.deltaTime);
        Vector3 pos = Vector3.MoveTowards(transform.position, transform_Player.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(transform_Player);

        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}