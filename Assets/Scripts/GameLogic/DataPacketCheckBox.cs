using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPacketCheckBox : MonoBehaviour {

    public bool HasDataPacket;
	// Use this for initialization
	void Start ()
    {
        HasDataPacket = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "DataPacket")
        {
            HasDataPacket = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "DataPacket")
        {
            HasDataPacket = false;
        }
    }
}
