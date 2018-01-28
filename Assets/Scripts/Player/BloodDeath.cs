using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDeath : MonoBehaviour {

    public GameObject blood;
   
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector3 pos = transform.position;
            pos.z = -2f;
            Instantiate(blood, pos, Quaternion.identity);
        }
	}

    public void Bleed()
    {
        Vector3 pos = transform.position;
        pos.z = -2f;
        Instantiate(blood, pos, Quaternion.identity);
    }
}
