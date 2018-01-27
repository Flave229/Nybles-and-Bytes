using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera : MonoBehaviour
{

    public int detectDistance;

    private GameObject player;
    private PlayerCTRL playerCTRL;
    private Vector3 playerPos;
    private Vector3 securityCameraPos;
    private Collider[] cols;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Player_Unique");
    }

    // Update is called once per frame
    void Update()
    {

        securityCameraPos = this.transform.position;
        cols = Physics.OverlapSphere(securityCameraPos, detectDistance);

        foreach (Collider col in cols)
        {
            if (col != null)
            {
                if (col.gameObject.GetComponent<UniquePlayerCTRL>() != null)
                {
                    col.gameObject.GetComponent<UniquePlayerCTRL>().DetectedByCamera();
                    return;
                }
                else
                {
                    // Clones

                }

            }
        }
    }
}




