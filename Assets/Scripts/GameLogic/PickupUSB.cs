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
            Vector3 scaler = holder.transform.localScale;
            pos.z = 0.01f;
            pos.y = 3.6f;
            transform.position = pos;

            scaler.x = 0.03f;
            scaler.y = 0.03f;

            transform.localScale = scaler;
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
