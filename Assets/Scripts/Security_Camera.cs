using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera : MonoBehaviour {

    public int detectDistance;

    private Vector3 playerPos;
    private Vector3 securityCameraPos;
    public GameObject[] objs; 

    // Use this for initialization
    void Start() {
        //player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update() {
        
        securityCameraPos = this.transform.position;
        objs = GameObject.FindGameObjectsWithTag("Player");

        foreach (var obj in objs)
        {
            if (obj != null)
            {
                playerPos = obj.transform.position;
                //        Debug.Log("Me Pos:" + securityCameraPos + " - Player Pos:" + playerPos.x);

                float dist = Vector3.Distance(securityCameraPos, playerPos);
                if (dist <= detectDistance)
                {
                    Debug.Log("Seen Distance:" + dist);
                }
            }
        }
    }
}



