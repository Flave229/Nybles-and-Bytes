using Assets.Scripts;
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
    private GameManager _gameManager;
    private int _mCloneIndex;
    private PlayerCameraController _mCamera;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Player_Unique");
        _gameManager = GameManager.Instance();
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
                    GameObject tempRef = _gameManager.GetListOfEntities()[_mCloneIndex].gameObject;
                    _gameManager.GetListOfEntities().Remove(_gameManager.GetListOfEntities()[_mCloneIndex]);
                    Destroy(tempRef, 0.0f);

                    _mCloneIndex -= 1;
                    _mCamera.SetTargetPlayerObject(_gameManager.GetListOfEntities()[_mCloneIndex]);
                    _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
                }

            }
        }
    }
}




