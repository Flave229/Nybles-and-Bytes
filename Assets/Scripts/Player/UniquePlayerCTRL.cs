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
        // If a clone is destroyed by pressing K, the index stored in here will be out of date.
        // Realign it here.
        while (GameManager.Instance().GetListOfEntities().Count - 1 < _mCloneControlIndex)
        {
            _mCloneControlIndex--;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(false);

            _mCloneControlIndex -= 1;
            if (_mCloneControlIndex < 0) _mCloneControlIndex = GameManager.Instance().GetListOfEntities().Count - 1;

            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);

            //Debug.Log("Idx: " + _mCloneControlIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(false);

            _mCloneControlIndex += 1;
            if (_mCloneControlIndex > GameManager.Instance().GetListOfEntities().Count - 1) _mCloneControlIndex = 0;

            GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);
            _mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);

            //Debug.Log("Idx: " + _mCloneControlIndex);
        }
    }

    public PlayerCTRL CreateClone(Vector3 spawnPoint)
    {
        GameObject tempRef = Instantiate(_mClonePlayersRef, spawnPoint, transform.rotation);
        PlayerCTRL tempRefPlayerCTRL = tempRef.GetComponent<PlayerCTRL>();
        GameManager.Instance().GetListOfEntities().Add(tempRefPlayerCTRL);
        tempRefPlayerCTRL.SetUserControlEnabled(false);
        return tempRefPlayerCTRL;
    }

	// does not disable the previously possessed clone
	public void PossessClone(PlayerCTRL clone)
	{
		_mCloneControlIndex = GameManager.Instance().GetListOfEntities().IndexOf(clone);

		GameManager.Instance().GetListOfEntities()[_mCloneControlIndex].SetUserControlEnabled(true);
		_mCamera.SetTargetPlayerObject(GameManager.Instance().GetListOfEntities()[_mCloneControlIndex]);
	}

    public void DetectedByCamera()
    {
        _mRefToMyself.DetectableBehaviour.Detected();

        Debug.Log("Detected");
    }
}
