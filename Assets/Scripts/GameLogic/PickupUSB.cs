using Assets.Scripts.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUSB : MonoBehaviour {

    Collider Collider;
    bool PickedUp = false;
    GameObject holder;

    Vector3 originalScale;
	// Use this for initialization
	void Start()
	{
        Collider = GetComponent<BoxCollider>();
        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (PickedUp)
        {
            GetComponent<Rigidbody>().useGravity = false;
            //if (transform.localScale.x > 0.15f)
            //	transform.localScale-=new Vector3(0.01f, 0.01f, 0);
            //transform.localPosition = new Vector3(0, 6.6666f*(1-2.2222f*(transform.localScale.x-0.15f)) );

            if (transform.localPosition.y < 6.66f)
                transform.localPosition = new Vector3(0, transform.localPosition.y + 0.33f);
            else
                transform.localPosition = new Vector3(0, 6.66f);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            
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

    public void DropItem()
    {
        PickedUp = false;

        gameObject.transform.parent = gameObject.transform.parent.parent;
    }
}
