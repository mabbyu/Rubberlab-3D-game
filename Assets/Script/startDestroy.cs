using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDestroy : MonoBehaviour
{
	public GameObject desObj;
    
	public float maxWaitTime;
	private float waitTime;

	// Start is called before the first frame update
    void Start()
    {
		if(desObj == null)
			desObj = gameObject;
		waitTime = maxWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
		waitTime -= Time.deltaTime * 1;
		if(waitTime <= 0)
			callFunct();
    }

	void callFunct()
	{
		//yield return new WaitForSeconds(waitTime);
		Destroy(desObj);
	}
}
