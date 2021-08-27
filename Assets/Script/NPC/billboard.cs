using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    private Camera theCam;

    public bool useStaticBillboard;     

    void Start()
    {
        theCam = Camera.main;
    }

    void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(theCam.transform);
        }
        else
        {
            transform.rotation = theCam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}