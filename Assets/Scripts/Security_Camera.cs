using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_Camera : MonoBehaviour
{
    public int detectDistance;

    private Vector3 securityCameraPos;
    private Collider[] cols;
    private GameManager _gameManager;
    private int _mCloneIndex;

    [SerializeField]
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
        securityCameraPos = transform.position;
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
                    if (col.gameObject.GetComponent<PlayerCTRL>() != null)
                    {
                        col.gameObject.GetComponent<PlayerCTRL>().DetectableBehaviour.Detected();

                        _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;
                        _mCloneIndex -= 1;
                        _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);
                        GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);

                    }
                }
            }
        }
    }
}




