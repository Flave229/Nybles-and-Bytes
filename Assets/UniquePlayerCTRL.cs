using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniquePlayerCTRL : MonoBehaviour
{
    [SerializeField]
    private GameObject _mClonePlayersRef;
    [SerializeField]
    private PlayerCameraController _mCamera;
    private List<PlayerCTRL> _mListOfPlayers;
    private PlayerCTRL _mRefToMyself;
	public int _mCloneIndex;

    private void Awake()
    {      
    }

    // Use this for initialization
    void Start()
    {
        _mCloneIndex = 0;
        _mRefToMyself = this.GetComponent<PlayerCTRL>();
        _mListOfPlayers = new List<PlayerCTRL>();
        _mListOfPlayers.Add(_mRefToMyself);
        _mRefToMyself.DetectableBehaviour = new PlayerDetected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            _mListOfPlayers[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex -= 1;
            if (_mCloneIndex < 0) _mCloneIndex = _mListOfPlayers.Count - 1;

            _mListOfPlayers[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(_mListOfPlayers[_mCloneIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            _mListOfPlayers[_mCloneIndex].SetUserControlEnabled(false);

            _mCloneIndex += 1;
            if (_mCloneIndex > _mListOfPlayers.Count - 1) _mCloneIndex = 0;

            _mListOfPlayers[_mCloneIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(_mListOfPlayers[_mCloneIndex]);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (_mListOfPlayers[_mCloneIndex] != _mRefToMyself)
            {
                GameObject tempRef = _mListOfPlayers[_mCloneIndex].gameObject;
                _mListOfPlayers.Remove(_mListOfPlayers[_mCloneIndex]);
                Destroy(tempRef, 0.0f);

                _mCloneIndex -= 1;
                _mCamera.SetTargetPlayerObject(_mListOfPlayers[_mCloneIndex]);
                _mListOfPlayers[_mCloneIndex].SetUserControlEnabled(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Insert))
        {
            Vector3 spawnPoint = _mListOfPlayers[_mCloneIndex].transform.position;
            spawnPoint.x += 5;
			CreateClone (spawnPoint);
        }
    }

	public void CreateClone(Vector3 spawnPoint)
	{
		GameObject tempRef = Instantiate(_mClonePlayersRef, spawnPoint, transform.rotation);
		PlayerCTRL tempRefPlayerCTRL = tempRef.GetComponent<PlayerCTRL>();
		_mListOfPlayers.Add(tempRefPlayerCTRL);
		tempRefPlayerCTRL.SetUserControlEnabled(false);

		_mListOfPlayers[_mCloneIndex].SetUserControlEnabled(false);

		_mCloneIndex++;

		_mCamera.SetTargetPlayerObject(_mListOfPlayers[_mCloneIndex]);
		_mListOfPlayers[_mCloneIndex].SetUserControlEnabled(true);
	}

    public void DetectedByCamera()
    {
        _mRefToMyself.DetectableBehaviour.Detected();
    }
}
