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
    private int _mCloneIndex;
    private int _newCloneXOffset;

    private void Awake()
    {
        _newCloneXOffset = 5;
    }

    // Use this for initialization
    void Start()
    {
        //_gameManager = GameManager.Instance();
        _mCloneIndex = 0;
        _mRefToMyself = this.GetComponent<PlayerCTRL>();
        GameManager.Instance().AddEntityToList(_mRefToMyself);
        _mRefToMyself.DetectableBehaviour = new PlayerDetected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex -= 1;
            if (_mCloneIndex < 0) _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;

            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);

            Debug.Log("Idx: " + _mCloneIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex += 1;
            if (_mCloneIndex > GameManager.Instance().GetListOfEntities().Count - 1) _mCloneIndex = 0;

            GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);

            Debug.Log("Idx: " + _mCloneIndex);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (GameManager.Instance().GetListOfEntities()[_mCloneIndex] != _mRefToMyself)
            {


                _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;
                Debug.Log("Del Idx: " + _mCloneIndex);

                GameObject tempRef = GameManager.Instance().GetListOfEntities()[_mCloneIndex].gameObject;
                GameManager.Instance().GetListOfEntities().Remove(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);
                Destroy(tempRef, 0.0f);

                // Move focus to another clone
                _mCloneIndex -= 1;
                _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneIndex]);
                GameManager.Instance().GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);

                Debug.Log("Focus Idx: " + _mCloneIndex);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Insert))
        {
            _mCloneIndex = GameManager.Instance().GetListOfEntities().Count - 1;
            Vector3 spawnPoint = GameManager.Instance().GetListOfEntities()[_mCloneIndex].transform.position;
            spawnPoint.x += _newCloneXOffset;
            CreateClone(spawnPoint);
        }
        else if (Input.GetKeyDown(KeyCode.Home))
        {
            // Switch Position Infront or Behind
//            if (Input.GetKeyDown(KeyCode.Insert))
//            {
//                Debug.Log("LeftShift + Insert");
//            }

            Debug.Log("Home");
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
