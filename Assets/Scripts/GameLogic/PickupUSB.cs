using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUSB : MonoBehaviour {

    Collider Collider;
    bool PickedUp = false;
    GameObject holder;
	// Use this for initialization
	void Start () {
        Collider = GetComponent<BoxCollider>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (PickedUp)
        {
            Vector3 pos =  holder.transform.position;
            transform.position = pos;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PickedUp = true;
            holder = col.gameObject;
        }
    }
}
