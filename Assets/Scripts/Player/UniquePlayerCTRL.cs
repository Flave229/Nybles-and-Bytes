using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniquePlayerCTRL : MonoBehaviour
{
    [SerializeField]
    private GameObject _mClonePlayersRef;
    [SerializeField]
    private PlayerCameraController _mCamera;
    private PlayerCTRL _mRefToMyself;
    private int _mCloneControlIndex;
    private int _newCloneXOffset;

    private void Awake()
    {
        _newCloneXOffset = 5;
    }

    // Use this for initialization
    void Start()
    {
        //_gameManager = GameManager.Instance();
        _mCloneControlIndex = 0;

        _mRefToMyself = this.GetComponent<PlayerCTRL>();
        GameManager.Instance().AddEntityToList(_mRefToMyself);
        _mRefToMyself.DetectableBehaviour = new PlayerDetected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(false);

            _mCloneControlIndex -= 1;
            if (_mCloneControlIndex < 0) _mCloneControlIndex = GameManager.Instance().GetListOfEntities().Count - 1;

            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);

            Debug.Log("Idx: " + _mCloneControlIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(false);

            _mCloneControlIndex += 1;
            if (_mCloneControlIndex > GameManager.Instance().GetListOfEntities().Count - 1) _mCloneControlIndex = 0;

            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);

            Debug.Log("Idx: " + _mCloneControlIndex);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (GameManager.Instance().GetListOfEntities()[_mCloneControlIndex] != _mRefToMyself)
            {
                _mCloneControlIndex = GameManager.Instance().GetListOfEntities().Count - 1;
                Debug.Log("Del Idx: " + _mCloneControlIndex);

                GameObject tempRef = GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].gameObject;
                GameManager.Instance().GetListOfEntities().Remove(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);
                Destroy(tempRef, 0.0f);

                // Move focus to another clone
                _mCloneControlIndex -= 1;
                _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);
                GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);

                Debug.Log("Focus Idx: " + _mCloneControlIndex);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Insert))
        {
            Vector3 spawnPoint = GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].transform.position;
            spawnPoint.x += _newCloneXOffset;
            CreateClone(spawnPoint);
        }
    }

    public void CreateClone(Vector3 spawnPoint)
    {
        GameObject tempRef = Instantiate(_mClonePlayersRef, spawnPoint, transform.rotation);
        PlayerCTRL tempRefPlayerCTRL = tempRef.GetComponent<PlayerCTRL>();
        GameManager.Instance().GetListOfEntities().Add(tempRefPlayerCTRL);
        tempRefPlayerCTRL.SetUserControlEnabled(false);
    }

    public void DetectedByCamera()
    {
        _mRefToMyself.DetectableBehaviour.Detected();

        Debug.Log("Detected");
    }
}
