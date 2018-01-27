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
    private GameManager _gameManager;

    private void Awake()
    {      
    }

    // Use this for initialization
    void Start()
    {
        _gameManager = GameManager.Instance();
        _mCloneIndex = 0;
        _mRefToMyself = this.GetComponent<PlayerCTRL>();
        _gameManager.AddEntityToList(_mRefToMyself);
        _mRefToMyself.DetectableBehaviour = new PlayerDetected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex -= 1;
            if (_mCloneIndex < 0) _mCloneIndex = _gameManager.GetListOfEntities().Count - 1;

            _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(_gameManager.GetListOfEntities()[_mCloneIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex += 1;
            if (_mCloneIndex > _gameManager.GetListOfEntities().Count - 1) _mCloneIndex = 0;

            _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(_gameManager.GetListOfEntities()[_mCloneIndex]);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (_gameManager.GetListOfEntities()[_mCloneIndex] != _mRefToMyself)
            {
                GameObject tempRef = _gameManager.GetListOfEntities()[_mCloneIndex].gameObject;
                _gameManager.GetListOfEntities().Remove(_gameManager.GetListOfEntities()[_mCloneIndex]);
                Destroy(tempRef, 0.0f);

                _mCloneIndex -= 1;
                _mCamera.SetTargetPlayerObject(_gameManager.GetListOfEntities()[_mCloneIndex]);
                _gameManager.GetListOfEntities()[_mCloneIndex].SetUserControlEnabled(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Insert))
        {
            Vector3 spawnPoint = _gameManager.GetListOfEntities()[_mCloneIndex].transform.position;
            spawnPoint.x += 5;
			CreateClone (spawnPoint);
        }
    }

	public void CreateClone(Vector3 spawnPoint)
	{
		GameObject tempRef = Instantiate(_mClonePlayersRef, spawnPoint, transform.rotation);
		PlayerCTRL tempRefPlayerCTRL = tempRef.GetComponent<PlayerCTRL>();
        _gameManager.GetListOfEntities().Add(tempRefPlayerCTRL);
		tempRefPlayerCTRL.SetUserControlEnabled(false);
	}

    public void DetectedByCamera()
    {
        _mRefToMyself.DetectableBehaviour.Detected();
    }
}
