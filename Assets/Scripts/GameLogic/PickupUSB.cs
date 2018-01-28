using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUSB : MonoBehaviour {

    Collider Collider;
    bool PickedUp = false;
    GameObject holder;
	// Use this for initialization
	void Start()
	{
        Collider = GetComponent<BoxCollider>();

    }
	
	// Update is called once per frame
	void Update()
    {
        if (PickedUp)
		{
			if (transform.localScale.x > 0.15f)
				transform.localScale-=new Vector3(0.01f, 0.01f, 0);

			transform.localPosition = new Vector3(0, 6.6666f*(1-2.2222f*(transform.localScale.x-0.15f)) );
		}
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PickedUp = true;
            holder = col.gameObject;
			gameObject.transform.parent = holder.transform;
		}
    }
}
